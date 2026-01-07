using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DAO;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private UserDAO userDAO = new UserDAO();
        private BookDAO bookDAO = new BookDAO();
        private LoanDAO loanDAO = new LoanDAO();

        public Form1()
        {
            InitializeComponent();
            LoadUsers();
            LoadBooks();
            LoadStatus();
        }

        private void LoadUsers()
        {
            List<User> users = userDAO.GetAll();
            UserInput.DataSource = users;
            UserInput.DisplayMember = "Name";
            UserInput.ValueMember = "Id";
        }

        private void LoadBooks()
        {
            List<Book> books = bookDAO.GetAll();
            BookInput.DataSource = books;
            BookInput.DisplayMember = "Title";
            BookInput.ValueMember = "Id";
        }

        private void LoadStatus()
        {
            LoanStatusInput.DataSource = Enum.GetValues(typeof(LoanStatus));
        }

        private void SubmitBTN_Click(object sender, EventArgs e)
        {
            try
            {
                User selectedUser = (User)UserInput.SelectedItem;
                Book selectedBook = (Book)BookInput.SelectedItem;
                LoanStatus status = (LoanStatus)LoanStatusInput.SelectedItem;

                DateTime loanDate = LoanDateInput.Value;
                DateTime? returnDate = ReturnDateInput.Value;

                Loan newLoan = new Loan(selectedUser.Id, selectedBook.Id, loanDate, returnDate, status);
                loanDAO.Save(newLoan);

                MessageBox.Text = "Loan successfully created";
            }
            catch (Exception ex) 
            {
                MessageBox.Text = "Failed \n" + ex.Message;
            }
        }









        //Can't be deleted
        private void UserInput_SelectedIndexChanged(object sender, EventArgs e){}
        private void BookInput_SelectedIndexChanged(object sender, EventArgs e){}
        private void label1_Click(object sender, EventArgs e){}
        private void label5_Click(object sender, EventArgs e){}
    }
}
