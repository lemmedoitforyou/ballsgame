using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PopadaiVKulkyGame
{
    public partial class MainForm : Form
    {
        private Timer timer;
        private int score;
        private int hearts;
        private Random coordinateRandom;
        private Random diameterRandom;
        private Random colorPickerRandom;
        private int timeBetweenAppearances;
        private int timeToClick;
        private const int maxBallSize = 100;
        private const int minBallSize = 20;
        private int ballsClicked;
        private int currentMinBallSize;
        public string userName { get; set; }
        private const string recordsFile = @"..\..\Records.csv";
        public bool IsLogined { get; set; }
        public MainForm()
        {
            userName = "";
            IsLogined = false;
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            Controls.Remove(gameOverLabel);
            Controls.Remove(mainMenuButton);
            Controls.Remove(scoreLabel);
            Controls.Remove(heartsLabel);
            score = 0;
            hearts = 3;
            ballsClicked = 0;
            currentMinBallSize = maxBallSize;
            timeBetweenAppearances = 2000;
            timeToClick = 2000;
            coordinateRandom = new Random();
            diameterRandom = new Random();
            colorPickerRandom = new Random();

            timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += Timer_Tick;

            #region RecordTable Values Adding
            recordsTable.Rows.Clear();
            recordsTable.Columns.Clear();
            recordsTable.Columns.Add("Place", "Place");
            recordsTable.Columns.Add("Name", "Name");
            recordsTable.Columns.Add("Score", "Score");
            recordsTable.Columns.Add("Date", "Date");

            List<List<string>> userRecords = new List<List<string>>();

            using (StreamReader reader = new StreamReader(recordsFile)) 
            {
                reader.ReadLine();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');

                    if (fields.Length == 3)
                    {
                        string name = fields[0];
                        string date = fields[1];
                        string recordsScore = fields[2];
                        userRecords.Add(new List<string> { name, recordsScore, date });
                    }
                }
            }

            userRecords.Sort((x, y) => Convert.ToInt32(y[1]).CompareTo(Convert.ToInt32(x[1])));
            
            for (int i =  0; i < 10; i++)
            {
                if (i < userRecords.Count)
                {
                    recordsTable.Rows.Add((i+1).ToString(), userRecords[i][0], userRecords[i][1], userRecords[i][2]);
                }
            }
            #endregion RecordTable Values Adding
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Controls.Remove(startButton);
            Controls.Remove(loginButton);
            Controls.Remove(recordsTable);
            Controls.Remove(recordsButton);
            Controls.Remove(currentUserLabel);
            timer.Start();
            Cursor = Cursors.Cross;
            Controls.Add(scoreLabel);
            Controls.Add(heartsLabel);
            scoreLabel.Visible = true;
            heartsLabel.Visible = true;
            UpdateHeartsLabel();
            UpdateScoreLabel();
        }

        private void UpdateHeartsLabel()
        {
            heartsLabel.Text = "Hearts: ";
            for (int i = 0; i < hearts; i++)
            {
                heartsLabel.Text += "♥";
            }
        }

        private void UpdateScoreLabel()
        {
            scoreLabel.Text = $"Score: {score}";
        }

        private void Ball_Click(object sender, EventArgs e)
        {
            var ball = (Ball)sender;
            var ballTimer = (Timer)ball.Tag; 
            ballTimer.Stop();

            ball.ScoreByColor(ref hearts, ref score);
            UpdateHeartsLabel();
            if (hearts == 0)
            {
                GameOver();
            }

            Controls.Remove(ball);
            UpdateScoreLabel();
            if (ballsClicked % 2 == 0)
            {
                if (timeToClick > 1000)
                {
                    timeBetweenAppearances = timeBetweenAppearances - (int)timeBetweenAppearances / 10;
                    timeToClick = timeBetweenAppearances;
                }
            }
            if(ballsClicked % 3 == 0)
            {
                if(currentMinBallSize > minBallSize)
                {
                    currentMinBallSize = (int)(currentMinBallSize - currentMinBallSize / 10);
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int diameter = diameterRandom.Next(currentMinBallSize, maxBallSize);

            int x = coordinateRandom.Next(ClientSize.Width - 50 - diameter);
            int y = coordinateRandom.Next(ClientSize.Height - 50 - diameter);
            
            var ball = new Ball(diameter, x, y, colorPickerRandom);
            ball.Click += Ball_Click;

            var ballTimer = new Timer();
            ballTimer.Interval = timeToClick;
            ballTimer.Tick += BallTimer_Tick;
            ballTimer.Tag = ball;

            ball.Tag = ballTimer;
            Controls.Add(ball);

            ballTimer.Start();

            timer.Interval = timeBetweenAppearances; 
        }

        private void BallTimer_Tick(object sender, EventArgs e)
        {
            var ballTimer = (Timer)sender;
            ballTimer.Stop();
            ballTimer.Tick -= BallTimer_Tick; 

            var ball = (Ball)ballTimer.Tag;
            Controls.Remove(ball);
            ball.BallDisappearHpHandler(ref hearts, ref score);
            UpdateScoreLabel();
            ball.Dispose(); 

            UpdateHeartsLabel();

            if (hearts == 0)
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            timer.Stop();
            Cursor = Cursors.Default;
            Controls.Clear();

            Controls.Add(gameOverLabel);
            Controls.Add(mainMenuButton);
            gameOverLabel.Text = $"Game over \nYour score: {score}";
            if (IsLogined == true)
            {
                string[] lines = File.ReadAllLines(recordsFile);

                bool userExists = false;
                int userIndex = -1;
                DateTime date = DateTime.Now;

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(',');

                    if (fields.Length >= 3 && fields[0] == userName)
                    {
                        userExists = true;
                        userIndex = i;

                        int existingScore = int.Parse(fields[2]);

                        if (score <= existingScore)
                        {
                            return;
                        }

                        break;
                    }
                }

                string recordLine = $"{userName},{date},{score}";

                if (userExists)
                {
                    lines[userIndex] = recordLine;
                }
                else
                {
                    Array.Resize(ref lines, lines.Length + 1);
                    lines[lines.Length - 1] = recordLine;
                }

                File.WriteAllLines(recordsFile, lines);
            }
        }

        private void playAgainButton_Click(object sender, EventArgs e)
        {
            Controls.Clear();
            Controls.Add(startButton);
            Controls.Add(loginButton);
            Controls.Add(recordsTable);
            Controls.Add(recordsButton);
            Controls.Add(currentUserLabel); 
            InitializeGame();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm(this);
            loginForm.ShowDialog();
            if (IsLogined)
            {
                currentUserLabel.Text = $"Current User:\n{userName}";
            }
        }

        private void recordsButton_Click(object sender, EventArgs e)
        {
            RecordsForm recordsForm = new RecordsForm();
            recordsForm.ShowDialog();
        }

    }
}
