namespace PopadaiVKulkyGame
{
    partial class RecordsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.recordsTable = new System.Windows.Forms.DataGridView();
            this.mainMenuButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.recordsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // recordsTable
            // 
            this.recordsTable.AllowUserToAddRows = false;
            this.recordsTable.AllowUserToDeleteRows = false;
            this.recordsTable.AllowUserToResizeColumns = false;
            this.recordsTable.AllowUserToResizeRows = false;
            this.recordsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.recordsTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.recordsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.recordsTable.Location = new System.Drawing.Point(12, 42);
            this.recordsTable.Name = "recordsTable";
            this.recordsTable.ReadOnly = true;
            this.recordsTable.RowHeadersVisible = false;
            this.recordsTable.RowHeadersWidth = 62;
            this.recordsTable.RowTemplate.Height = 24;
            this.recordsTable.Size = new System.Drawing.Size(780, 334);
            this.recordsTable.TabIndex = 0;
            // 
            // mainMenuButton
            // 
            this.mainMenuButton.Location = new System.Drawing.Point(327, 391);
            this.mainMenuButton.Name = "mainMenuButton";
            this.mainMenuButton.Size = new System.Drawing.Size(109, 47);
            this.mainMenuButton.TabIndex = 1;
            this.mainMenuButton.Text = "Main Menu";
            this.mainMenuButton.UseVisualStyleBackColor = true;
            this.mainMenuButton.Click += new System.EventHandler(this.mainMenuButton_Click);
            // 
            // RecordsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainMenuButton);
            this.Controls.Add(this.recordsTable);
            this.Name = "RecordsForm";
            this.Text = "RecordsForm";
            ((System.ComponentModel.ISupportInitialize)(this.recordsTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView recordsTable;
        private System.Windows.Forms.Button mainMenuButton;
    }
}