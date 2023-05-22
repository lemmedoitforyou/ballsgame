using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PopadaiVKulkyGame
{
    public partial class LoginForm : Form
    {
        private MainForm mainForm;
        private const string playersFile = "C:\\Users\\Adminnn\\Desktop\\папочка\\унік\\c#\\New folder\\PopadaiVKulkyGame\\PopadaiVKulkyGame\\Players.csv";
        public LoginForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void userNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if(userNameTextBox.Text != "")
            {
                userNameTextBox.BackColor = Color.White;
            }
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            if(passwordTextBox.Text != "")
            {
                passwordTextBox.BackColor = Color.White;
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string name = userNameTextBox.Text;
            string password = passwordTextBox.Text;
            
            if (name == "")
            {
                userNameTextBox.BackColor = Color.Red;
            }
            if (password == "")
            {
                passwordTextBox.BackColor = Color.Red;
            } 

            bool nameExists = CheckIfNameExists(name);

            if (nameExists)
            {
                bool passwordMatches = CheckPassword(name, password);

                if (passwordMatches)
                {
                    MessageBox.Show("Login successful!");
                }
                else
                {
                    MessageBox.Show("Invalid password!");
                    return;
                }
            }
            else
            {
                AddNewPlayer(name, password);
                MessageBox.Show("New player added!");
            }

            mainForm.IsLogined = true;
            mainForm.userName = userNameTextBox.Text;
            this.Close();
        }

        private bool CheckIfNameExists(string name)
        {
            using (StreamReader reader = new StreamReader(playersFile))
            {
                reader.ReadLine();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');
                    if (fields.Length >= 2 && fields[0] == name)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckPassword(string name, string password)
        {
            using (StreamReader reader = new StreamReader(playersFile))
            {
                reader.ReadLine();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');
                    if (fields.Length >= 2 && fields[0] == name && fields[1] == password)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void AddNewPlayer(string name, string password)
        {
            using (StreamWriter writer = new StreamWriter(playersFile, true))
            {
                writer.WriteLine($"{name},{password}");
            }
        }
    }
}
