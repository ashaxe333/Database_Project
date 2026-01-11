using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp1.DAO;
using WindowsFormsApp1.Database;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private UserDAO userDAO = new UserDAO();
        private BookDAO bookDAO = new BookDAO();
        private LoanDAO loanDAO = new LoanDAO();

        private AuthorDAO authorDAO = new AuthorDAO();
        private Book_AuthorsDAO book_AuthorsDAO = new Book_AuthorsDAO();

        private ActiveLoanDAO activeLoanDAO = new ActiveLoanDAO();
        private PaymentSummaryDAO paymentSummaryDAO = new PaymentSummaryDAO();

        public Form1()
        {
            InitializeComponent();
            LoadUsers();
            LoadBooks();
            LoadStatus();
        }

        /// <summary>
        /// Loads names of all users in database
        /// </summary>
        private void LoadUsers()
        {
            List<User> users = userDAO.GetAll();
            UserInput.DataSource = users;
            UserInput.DisplayMember = "Name";
            UserInput.ValueMember = "Id";
        }

        /// <summary>
        /// Loads titles of all books in database
        /// </summary>
        private void LoadBooks()
        {
            List<Book> books = bookDAO.GetAll();
            BookInput.DataSource = books;
            BookInput.DisplayMember = "Title";
            BookInput.ValueMember = "Id";
        }

        /// <summary>
        /// Loads status
        /// </summary>
        private void LoadStatus()
        {
            LoanStatusInput.DataSource = Enum.GetValues(typeof(LoanStatus));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"> SubmitBTN button </param>
        /// <param name="e"> click </param>
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
                loanDAO.Save(newLoan, null);

                MessageBox.Text = "Loan successfully created!";
            }
            catch (Exception ex) 
            {
                MessageBox.Text = "Failed \n" + ex.Message;
            }
        }

        /// <summary>
        /// Inserts new book, author and inserts new book_authors row
        /// </summary>
        /// <param name="sender"> Submit2 button </param>
        /// <param name="e"> click </param>
        private void Submit2_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = DatabaseSingleton.GetInstance();
            MySqlTransaction transaction = null;

            try
            {
                transaction = conn.BeginTransaction();

                string authorName = AuthorNameInput.Text;
                string bookTitle = BookTitleInput.Text;
                int bookPublishedYear = int.Parse(BookPublishedYearInput.Text);

                if(!Regex.IsMatch(authorName, "^[A-Za-zěščřžýáíéóúůďťňĚŠČŘŽÝÁÍÉÓÚŮĎŤŇ ']+$"))
                {
                    throw new Exception("Invalid name");
                }

                Book newBook = new Book(bookTitle, bookPublishedYear, true);
                Author newAuthor = new Author(authorName);

                bookDAO.Save(newBook, transaction);
                authorDAO.Save(newAuthor, transaction);

                Book_Authors newBA = new Book_Authors(newBook.Id, newAuthor.Id);
                book_AuthorsDAO.Save(newBA, transaction);

                transaction.Commit();
                MessageBox.Text = "Book and Author successfully added!";
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Text = "Failed \n" + ex.Message;
            }
        }

        /// <summary>
        /// calls import method in bookDAO, and sends it path to csv file
        /// </summary>
        /// <param name="sender"> ImportToBooks button </param>
        /// <param name="e"> click </param>
        private void ImportToBooks_Click(object sender, EventArgs e)
        {
            try
            {
                bookDAO.importCSV(Path.Text);
                MessageBox.Text = "import was succesful!";
            }
            catch (Exception ex)
            {
                MessageBox.Text = "Failed \n" + ex.Message;
            }
        }

        /// <summary>
        /// calls import method in authorDAO, and sends it path to csv file
        /// </summary>
        /// <param name="sender"> ImportToAuthors button </param>
        /// <param name="e"> click </param>
        private void ImportToAuthors_Click(object sender, EventArgs e)
        {
            try
            {
                authorDAO.importCSV(Path.Text);
                MessageBox.Text = "import was succesful!";
            }
            catch (Exception ex)
            {
                MessageBox.Text = "Failed \n" + ex.Message;
            }
        }

        /// <summary>
        /// writes all rows from view to output textbox
        /// </summary>
        /// <param name="sender"> ShowActiveLoans button </param>
        /// <param name="e"> click </param>
        private void ShowActiveLoans_Click(object sender, EventArgs e)
        {
            try 
            {
                List<ActiveLoan> activeLoans = activeLoanDAO.GetAll();
                string output = null;
                foreach (ActiveLoan activeLoan in activeLoans) output += activeLoan + "\n";

                MessageBox.Text = "output: \n" + output;
            }
            catch (Exception ex)
            {
                MessageBox.Text = "Failed \n" + ex.Message;
            }
        }

        /// <summary>
        /// writes all rows from view to output textbox
        /// </summary>
        /// <param name="sender"> ShowPaymentSummary button </param>
        /// <param name="e"> click </param>
        private void ShowPaymentSummary_Click(object sender, EventArgs e)
        {
            try
            {
                List<PaymentSummary> paymentSummaries = paymentSummaryDAO.GetAll();
                string output = null;
                foreach (PaymentSummary paymentSummary in paymentSummaries) output += paymentSummary + "\n";

                MessageBox.Text = "output: \n" + output;
            }
            catch (Exception ex)
            {
                MessageBox.Text = "Failed \n" + ex.Message;
            }
        }
    }
}
