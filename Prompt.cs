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

namespace Projekt
{
    public class Prompt
    {
        private UserDAO userDAO = new UserDAO();
        private BookDAO bookDAO = new BookDAO();
        private Book_AuthorsDAO book_authorsDAO = new Book_AuthorsDAO();
        private AuthorDAO authorDAO = new AuthorDAO();
        private LoanDAO loanDAO = new LoanDAO();
        private PaymentDAO paymentDAO = new PaymentDAO();

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
        /// Stará se o vše, co se děje v konzoli, plní příkazy
        /// </summary>
        public void Execute()
        {
            //userDAO.Save(new User("Jan Novak", "jan.novak@example.com", true, DateTime.Now));

            //userDAO.Delete(12);

            //Console.WriteLine(userDAO.GetById(1));

            //userDAO.Save(new User(13, "John Smith", "john.smith@example.com", true, DateTime.Now));

            List<User> users = userDAO.GetAll();
            foreach (User user in users)
            {
                Console.WriteLine(user);
            }
        }
    }
}
