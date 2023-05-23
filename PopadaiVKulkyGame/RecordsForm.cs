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
    public partial class RecordsForm : Form
    {
        private string recordsFile = @".\Records.csv";

        public RecordsForm()
        {
            InitializeComponent();
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

            for (int i = 0; i < userRecords.Count; i++)
            {
                if (i < userRecords.Count)
                {
                    recordsTable.Rows.Add((i + 1).ToString(), userRecords[i][0], userRecords[i][1], userRecords[i][2]);
                }
            }
        }

        private void mainMenuButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
