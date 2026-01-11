namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.UserInput = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BookInput = new System.Windows.Forms.ComboBox();
            this.LoanDateInput = new System.Windows.Forms.DateTimePicker();
            this.ReturnDateInput = new System.Windows.Forms.DateTimePicker();
            this.LoanStatusInput = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SubmitBTN = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ImportToBooks = new System.Windows.Forms.Button();
            this.ImportToAuthors = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.Path = new System.Windows.Forms.TextBox();
            this.MessageBox = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ShowPaymentSummary = new System.Windows.Forms.Button();
            this.ShowActiveLoans = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.Submit2 = new System.Windows.Forms.Button();
            this.BookTitleInput = new System.Windows.Forms.TextBox();
            this.BookTitle = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.BookPublishedYearInput = new System.Windows.Forms.TextBox();
            this.AuthorName = new System.Windows.Forms.Label();
            this.AuthorNameInput = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserInput
            // 
            this.UserInput.FormattingEnabled = true;
            this.UserInput.Location = new System.Drawing.Point(18, 58);
            this.UserInput.Name = "UserInput";
            this.UserInput.Size = new System.Drawing.Size(203, 24);
            this.UserInput.TabIndex = 0;
            this.UserInput.SelectedIndexChanged += new System.EventHandler(this.UserInput_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "User:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(277, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Book:";
            // 
            // BookInput
            // 
            this.BookInput.FormattingEnabled = true;
            this.BookInput.Location = new System.Drawing.Point(280, 58);
            this.BookInput.Name = "BookInput";
            this.BookInput.Size = new System.Drawing.Size(200, 24);
            this.BookInput.TabIndex = 3;
            this.BookInput.SelectedIndexChanged += new System.EventHandler(this.BookInput_SelectedIndexChanged);
            // 
            // LoanDateInput
            // 
            this.LoanDateInput.Location = new System.Drawing.Point(21, 117);
            this.LoanDateInput.Name = "LoanDateInput";
            this.LoanDateInput.Size = new System.Drawing.Size(200, 22);
            this.LoanDateInput.TabIndex = 4;
            // 
            // ReturnDateInput
            // 
            this.ReturnDateInput.Location = new System.Drawing.Point(280, 117);
            this.ReturnDateInput.Name = "ReturnDateInput";
            this.ReturnDateInput.Size = new System.Drawing.Size(200, 22);
            this.ReturnDateInput.TabIndex = 5;
            // 
            // LoanStatusInput
            // 
            this.LoanStatusInput.FormattingEnabled = true;
            this.LoanStatusInput.Location = new System.Drawing.Point(18, 180);
            this.LoanStatusInput.Name = "LoanStatusInput";
            this.LoanStatusInput.Size = new System.Drawing.Size(200, 24);
            this.LoanStatusInput.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Loan Date:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(277, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Return Date (optional):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Loan Status:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // SubmitBTN
            // 
            this.SubmitBTN.Location = new System.Drawing.Point(283, 225);
            this.SubmitBTN.Name = "SubmitBTN";
            this.SubmitBTN.Size = new System.Drawing.Size(197, 34);
            this.SubmitBTN.TabIndex = 10;
            this.SubmitBTN.Text = "Submit";
            this.SubmitBTN.UseVisualStyleBackColor = true;
            this.SubmitBTN.Click += new System.EventHandler(this.SubmitBTN_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SubmitBTN);
            this.groupBox1.Controls.Add(this.UserInput);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.BookInput);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.LoanDateInput);
            this.groupBox1.Controls.Add(this.LoanStatusInput);
            this.groupBox1.Controls.Add(this.ReturnDateInput);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(505, 278);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create Loan";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ImportToBooks);
            this.groupBox2.Controls.Add(this.ImportToAuthors);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.Path);
            this.groupBox2.Location = new System.Drawing.Point(12, 296);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(505, 137);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CSV Data Import";
            // 
            // ImportToBooks
            // 
            this.ImportToBooks.Location = new System.Drawing.Point(160, 86);
            this.ImportToBooks.Name = "ImportToBooks";
            this.ImportToBooks.Size = new System.Drawing.Size(130, 33);
            this.ImportToBooks.TabIndex = 3;
            this.ImportToBooks.Text = "Import to books";
            this.ImportToBooks.UseVisualStyleBackColor = true;
            this.ImportToBooks.Click += new System.EventHandler(this.ImportToBooks_Click);
            // 
            // ImportToAuthors
            // 
            this.ImportToAuthors.Location = new System.Drawing.Point(18, 86);
            this.ImportToAuthors.Name = "ImportToAuthors";
            this.ImportToAuthors.Size = new System.Drawing.Size(136, 33);
            this.ImportToAuthors.TabIndex = 2;
            this.ImportToAuthors.Text = "Import to authors";
            this.ImportToAuthors.UseVisualStyleBackColor = true;
            this.ImportToAuthors.Click += new System.EventHandler(this.ImportToAuthors_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Path/To/File:";
            // 
            // Path
            // 
            this.Path.Location = new System.Drawing.Point(18, 52);
            this.Path.Name = "Path";
            this.Path.Size = new System.Drawing.Size(462, 22);
            this.Path.TabIndex = 0;
            // 
            // MessageBox
            // 
            this.MessageBox.Location = new System.Drawing.Point(6, 21);
            this.MessageBox.Name = "MessageBox";
            this.MessageBox.Size = new System.Drawing.Size(526, 221);
            this.MessageBox.TabIndex = 14;
            this.MessageBox.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.MessageBox);
            this.groupBox3.Location = new System.Drawing.Point(523, 296);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(538, 248);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "OutPut";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ShowPaymentSummary);
            this.groupBox4.Controls.Add(this.ShowActiveLoans);
            this.groupBox4.Location = new System.Drawing.Point(12, 439);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(505, 105);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Views";
            // 
            // ShowPaymentSummary
            // 
            this.ShowPaymentSummary.Location = new System.Drawing.Point(175, 43);
            this.ShowPaymentSummary.Name = "ShowPaymentSummary";
            this.ShowPaymentSummary.Size = new System.Drawing.Size(179, 31);
            this.ShowPaymentSummary.TabIndex = 1;
            this.ShowPaymentSummary.Text = "Show Payment Summary";
            this.ShowPaymentSummary.UseVisualStyleBackColor = true;
            this.ShowPaymentSummary.Click += new System.EventHandler(this.ShowPaymentSummary_Click);
            // 
            // ShowActiveLoans
            // 
            this.ShowActiveLoans.Location = new System.Drawing.Point(18, 43);
            this.ShowActiveLoans.Name = "ShowActiveLoans";
            this.ShowActiveLoans.Size = new System.Drawing.Size(142, 31);
            this.ShowActiveLoans.TabIndex = 0;
            this.ShowActiveLoans.Text = "Show Active Loans";
            this.ShowActiveLoans.UseVisualStyleBackColor = true;
            this.ShowActiveLoans.Click += new System.EventHandler(this.ShowActiveLoans_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.AuthorNameInput);
            this.groupBox5.Controls.Add(this.AuthorName);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.BookPublishedYearInput);
            this.groupBox5.Controls.Add(this.BookTitle);
            this.groupBox5.Controls.Add(this.BookTitleInput);
            this.groupBox5.Controls.Add(this.Submit2);
            this.groupBox5.Location = new System.Drawing.Point(523, 13);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(538, 277);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Create new book with new author";
            // 
            // Submit2
            // 
            this.Submit2.Location = new System.Drawing.Point(315, 224);
            this.Submit2.Name = "Submit2";
            this.Submit2.Size = new System.Drawing.Size(197, 34);
            this.Submit2.TabIndex = 0;
            this.Submit2.Text = "Submit";
            this.Submit2.UseVisualStyleBackColor = true;
            this.Submit2.Click += new System.EventHandler(this.Submit2_Click);
            // 
            // BookTitleInput
            // 
            this.BookTitleInput.Location = new System.Drawing.Point(20, 59);
            this.BookTitleInput.Name = "BookTitleInput";
            this.BookTitleInput.Size = new System.Drawing.Size(150, 22);
            this.BookTitleInput.TabIndex = 4;
            this.BookTitleInput.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // BookTitle
            // 
            this.BookTitle.AutoSize = true;
            this.BookTitle.Location = new System.Drawing.Point(17, 38);
            this.BookTitle.Name = "BookTitle";
            this.BookTitle.Size = new System.Drawing.Size(71, 16);
            this.BookTitle.TabIndex = 5;
            this.BookTitle.Text = "Book Title:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(193, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Book published year (optional):";
            // 
            // BookPublishedYearInput
            // 
            this.BookPublishedYearInput.Location = new System.Drawing.Point(20, 118);
            this.BookPublishedYearInput.Name = "BookPublishedYearInput";
            this.BookPublishedYearInput.Size = new System.Drawing.Size(150, 22);
            this.BookPublishedYearInput.TabIndex = 6;
            // 
            // AuthorName
            // 
            this.AuthorName.AutoSize = true;
            this.AuthorName.Location = new System.Drawing.Point(264, 38);
            this.AuthorName.Name = "AuthorName";
            this.AuthorName.Size = new System.Drawing.Size(88, 16);
            this.AuthorName.TabIndex = 8;
            this.AuthorName.Text = "Author Name:";
            // 
            // AuthorNameInput
            // 
            this.AuthorNameInput.Location = new System.Drawing.Point(267, 59);
            this.AuthorNameInput.Name = "AuthorNameInput";
            this.AuthorNameInput.Size = new System.Drawing.Size(150, 22);
            this.AuthorNameInput.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1079, 556);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox UserInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox BookInput;
        private System.Windows.Forms.DateTimePicker LoanDateInput;
        private System.Windows.Forms.DateTimePicker ReturnDateInput;
        private System.Windows.Forms.ComboBox LoanStatusInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button SubmitBTN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox Path;
        private System.Windows.Forms.RichTextBox MessageBox;
        private System.Windows.Forms.Button ImportToAuthors;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button ImportToBooks;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button ShowPaymentSummary;
        private System.Windows.Forms.Button ShowActiveLoans;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button Submit2;
        private System.Windows.Forms.Label BookTitle;
        private System.Windows.Forms.TextBox BookTitleInput;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox BookPublishedYearInput;
        private System.Windows.Forms.TextBox AuthorNameInput;
        private System.Windows.Forms.Label AuthorName;
    }
}

