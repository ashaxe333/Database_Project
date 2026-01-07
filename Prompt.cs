using WindowsFormsApp1.DAO;
using WindowsFormsApp1.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.IsolatedStorage;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public class Prompt
    {
        private UserDAO userDAO = new UserDAO();
        private BookDAO bookDAO = new BookDAO();
        private Book_AuthorsDAO book_authorsDAO = new Book_AuthorsDAO();
        private AuthorDAO authorDAO = new AuthorDAO();
        private LoanDAO loanDAO = new LoanDAO();
        private PaymentDAO paymentDAO = new PaymentDAO();
        private PaymentSummaryDAO paymentSummaryDAO = new PaymentSummaryDAO();
        private ActiveLoanDAO activeLoanDAO = new ActiveLoanDAO();

        private List<string> commands = new List<string>() { "user", "book", "author", "borrows", "help", "exit"};
        private List<string> tableCommands = new List<string>() { "delete", "update", "insert", "help", "back", "import"};

        /// <summary>
        /// Vytvoří konzoli (Prompt) a spustí metodu Execute()
        /// </summary>
        public Prompt()
        {
            Execute();
        }

        /// <summary>
        /// Dubug method
        /// </summary>
        public void Execute()
        {
            // =========================
            // USER
            // =========================
            /*
            userDAO.Save(new User("Jan Novak", "jan.novak@example.com", true, DateTime.Now));
            userDAO.Delete(16);
            Console.WriteLine(userDAO.GetById(1));
            userDAO.Save(new User("John Smith", "john.smith@example.com", true, DateTime.Now));
            
            List<User> users = userDAO.GetAll();
            foreach (User user in users)
            {
                Console.WriteLine(user);
            }
            */

            // =========================
            // AUTHOR
            // =========================
            /*
            authorDAO.Save(new Author("Stephen King"));
            authorDAO.Save(new Author(11, "George Orwell"));
            authorDAO.Delete(11);
            Console.WriteLine(authorDAO.GetById(11));

            List<Author> authors = authorDAO.GetAll();
            foreach (Author author in authors)
                Console.WriteLine(author);
            */

            // =========================
            // BOOK
            // =========================
            /*
            bookDAO.Save(new Book("1985", null, true));
            bookDAO.Save(new Book(12, "1984", null, true));
            bookDAO.Delete(12);
            Console.WriteLine(bookDAO.GetById(10));

            List<Book> books = bookDAO.GetAll();
            foreach (Book book in books)
                Console.WriteLine(book);
            */

            // =========================
            // BOOK_AUTHORS (M:N)
            // =========================
            /*
            book_authorsDAO.Save(new Book_Authors(1, 2));
            book_authorsDAO.Save(new Book_Authors(12, 1, 3));
            book_authorsDAO.Delete(12);
            Console.WriteLine(book_authorsDAO.GetById(10));

            List<Book_Authors> bookAuthors = book_authorsDAO.GetAll();
            foreach (Book_Authors ba in bookAuthors)
                Console.WriteLine(ba);
            */

            // =========================
            // LOAN
            // =========================
            /*
            loanDAO.Save(new Loan(1, 1, DateTime.Now.AddDays(-7), DateTime.Now.AddDays(7), LoanStatus.RETURNED));
            loanDAO.Save(new Loan(13, 1, 1, DateTime.Now.AddDays(-7), DateTime.Now.AddDays(7), LoanStatus.RETURNED));
            loanDAO.Delete(14);
            Console.WriteLine(loanDAO.GetById(13));

            List<Loan> loans = loanDAO.GetAll();
            foreach (Loan loan in loans)
                Console.WriteLine(loan);
            */

            // =========================
            // PAYMENT
            // =========================
            /*
            paymentDAO.Save(new Payment(1, 9.99f, DateTime.Now));
            paymentDAO.Save(new Payment(8, 3, 9.99f, DateTime.Now));
            paymentDAO.Delete(8);
            Console.WriteLine(paymentDAO.GetById(8));

            List<Payment> payments = paymentDAO.GetAll();
            foreach (Payment payment in payments)
                Console.WriteLine(payment);
            */
            /*
            foreach (ActiveLoan activeLoan in activeLoanDAO.GetAll())
                Console.WriteLine(activeLoan);
            
            
            foreach (PaymentSummary paymentSummary in paymentSummaryDAO.GetAll())
                Console.WriteLine(paymentSummary);
            */

            Console.WriteLine("=== DATABASE TEST END ===");
        }
    }
}
