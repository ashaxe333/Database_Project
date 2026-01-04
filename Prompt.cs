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
        private GenreDAO genreDAO = new GenreDAO();
        private BookDAO bookDAO = new BookDAO();
        private AuthorDAO authorDAO = new AuthorDAO();
        private LoanDAO borrowsDAO = new LoanDAO();

        private List<string> commands = new List<string>() { "user", "genre", "book", "author", "borrows", "help", "exit"};
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
            Console.WriteLine("Select table by typing its name or type 'help' to show commands");
            bool repeat = true;
            do
            {
                Console.Write(">>");
                string answer = Console.ReadLine();

                bool repeat2 = true;
                while (repeat2)
                {
                    string answer2 = "";
                    switch (answer)
                    {
                        //GENRE
                        case "genre":
                            Console.WriteLine("Here you can use ONLY 'import'");
                            Console.Write($"{genreDAO.ToString()}>>");
                            answer2 = Console.ReadLine().Trim().ToLower();

                            if(answer2 == "help")
                            {
                                Console.WriteLine(WriteTableCommands());
                                break;
                            }
                            else if (answer2 == "back")
                            {
                                repeat2 = false;
                                break;
                            }
                            else if (answer2 == "import")
                            {
                                Console.WriteLine(genreDAO.importCSV());
                            }
                            else
                            {
                                Console.WriteLine("unknown command, you can type 'help' to show all commands");
                                break;
                            }
                            break;

                        //USER
                        case "user":
                            Console.Write($"{userDAO.ToString()}>>");
                            answer2 = Console.ReadLine().Trim().ToLower();

                            if (answer2 == "help")
                            {
                                Console.WriteLine(WriteTableCommands());
                                break;
                            }
                            else if (answer2 == "back")
                            {
                                repeat2 = false;
                                break;
                            }
                            else if (answer2 == "delete")
                            {
                                Console.WriteLine("id: ");
                                string answer3 = Console.ReadLine();
                                if (int.TryParse(answer3, out int id))
                                {
                                    userDAO.Delete(id);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid datatype");
                                    break;
                                }
                            }
                            else if (answer2 == "update")
                            {

                                Console.WriteLine("id: ");
                                string answer3 = Console.ReadLine();
                                Console.WriteLine("name: ");
                                string name = Console.ReadLine();
                                Console.WriteLine("email: ");
                                string email = Console.ReadLine();
                                if (int.TryParse(answer3, out int id))
                                {
                                    User user = new User(id, name, email);
                                    userDAO.Save(user);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid datatype");
                                    break;
                                }
                            }
                            else if (answer2 == "insert")
                            {
                                Console.WriteLine("name: ");
                                string name = Console.ReadLine();
                                Console.WriteLine("email: ");
                                string email = Console.ReadLine();
                                User user = new User(name, email);
                                userDAO.Save(user);
                            }
                            else
                            {
                                Console.WriteLine("unknown command, you can type 'help' to show all commands");
                                break;
                            }
                            break;

                        //AUTHOR
                        case "author":
                            Console.Write($"{authorDAO.ToString()}>>");
                            answer2 = Console.ReadLine().Trim().ToLower();

                            if (answer2 == "help")
                            {
                                Console.WriteLine(WriteTableCommands());
                                break;
                            }
                            else if (answer2 == "back")
                            {
                                repeat2 = false;
                                break;
                            }
                            else if (answer2 == "delete")
                            {
                                Console.WriteLine("id: ");
                                string answer3 = Console.ReadLine();
                                if (int.TryParse(answer3, out int id))
                                {
                                    authorDAO.Delete(id);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid datatype");
                                    break;
                                }
                            }
                            else if (answer2 == "update")
                            {
                                Console.WriteLine("id: ");
                                string answer3 = Console.ReadLine();
                                Console.WriteLine("name: ");
                                string name = Console.ReadLine();
                                if (int.TryParse(answer3, out int id))
                                {
                                    Author author = new Author(id, name);
                                    authorDAO.Save(author);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid datatype");
                                    break;
                                }
                            }
                            else if (answer2 == "insert")
                            {
                                Console.WriteLine("name: ");
                                string name = Console.ReadLine();
                                Author author = new Author(name);
                                authorDAO.Save(author);
                            }
                            else
                            {
                                Console.WriteLine("unknown command, you can type 'help' to show all commands");
                                break;
                            }
                            break;

                        //BOOK
                        case "book":
                            Console.Write($"{bookDAO.ToString()}>>");
                            answer2 = Console.ReadLine().Trim().ToLower();

                            if (answer2 == "help")
                            {
                                Console.WriteLine(WriteTableCommands());
                                break;
                            }
                            else if (answer2 == "back")
                            {
                                repeat2 = false;
                                break;
                            }
                            else if (answer2 == "import")
                            {
                                Console.WriteLine(bookDAO.importCSV());
                            }
                            else if (answer2 == "delete")
                            {
                                Console.WriteLine("id: ");
                                string answer3 = Console.ReadLine();
                                if (int.TryParse(answer3, out int id))
                                {
                                    bookDAO.Delete(id);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid datatype");
                                    break;
                                }
                            }
                            else if (answer2 == "update")
                            {
                                Console.WriteLine("id: ");
                                string answer3 = Console.ReadLine();
                                Console.WriteLine("author_id: ");
                                string answer4 = Console.ReadLine();
                                Console.WriteLine("genre_id: ");
                                string answer5 = Console.ReadLine();
                                Console.WriteLine("title: ");
                                string title = Console.ReadLine();
                                Console.WriteLine("is Available at the moment? (type 'true' or 'false'): ");
                                string answer6 = Console.ReadLine();
                                Console.WriteLine("price: ");
                                string answer7 = Console.ReadLine();

                                if (int.TryParse(answer3, out int id) && int.TryParse(answer4, out int author_id) && int.TryParse(answer5, out int genre_id) && bool.TryParse(answer6, out bool isAvailable) && float.TryParse(answer7, CultureInfo.InvariantCulture.NumberFormat, out float price) && price > 0)
                                {
                                    Book book = new Book(id, author_id, genre_id, title, isAvailable, price);
                                    bookDAO.Save(book);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid datatype");
                                    break;
                                }
                            }
                            else if (answer2 == "insert")
                            {
                                Console.WriteLine("type author_id: ");
                                string answer4 = Console.ReadLine();
                                Console.WriteLine("type genre_id: ");
                                string answer5 = Console.ReadLine();
                                Console.WriteLine("type title: ");
                                string title = Console.ReadLine();
                                Console.WriteLine("is Available at the moment? (type 'true' or 'false'): ");
                                string answer6 = Console.ReadLine();
                                Console.WriteLine("price: ");
                                string answer7 = Console.ReadLine();

                                if (int.TryParse(answer4, out int author_id) && int.TryParse(answer5, out int genre_id) && bool.TryParse(answer6, out bool isAvailable) && float.TryParse(answer7, CultureInfo.InvariantCulture.NumberFormat, out float price) && price > 0)
                                {
                                    Book book = new Book(author_id, genre_id, title, isAvailable, price);
                                    bookDAO.Save(book);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid datatype");
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("unknown command, you can type 'help' to show all commands");
                                break;
                            }
                            break;

                        case "borrows":
                            Console.Write($"{borrowsDAO.ToString()}>>");
                            answer2 = Console.ReadLine().Trim().ToLower();

                            if (answer2 == "help")
                            {
                                Console.WriteLine(WriteTableCommands());
                                break;
                            }
                            else if (answer2 == "back")
                            {
                                repeat2 = false;
                                break;
                            }
                            else if (answer2 == "delete")
                            {
                                Console.WriteLine("id: ");
                                string answer3 = Console.ReadLine();
                                if (int.TryParse(answer3, out int id))
                                {
                                    borrowsDAO.Delete(id);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid datatype");
                                    break;
                                }
                            }
                            else if (answer2 == "update")
                            {
                                Console.WriteLine("id: ");
                                string answer3 = Console.ReadLine();
                                Console.WriteLine("user_id: ");
                                string answer4 = Console.ReadLine();
                                Console.WriteLine("book_id: ");
                                string answer5 = Console.ReadLine();
                                Console.WriteLine("borrow date (dd/mm/yyyy): ");
                                string answer6 = Console.ReadLine();
                                Console.WriteLine("return date (dd/mm/yyyy): ");
                                string answer7 = Console.ReadLine();

                                if (int.TryParse(answer3, out int id) && int.TryParse(answer4, out int user_id) && int.TryParse(answer5, out int book_id) && DateTime.TryParse(answer6, out DateTime borrowDate) && DateTime.TryParse(answer7, out DateTime returnDate))
                                {
                                    Loan borrows = new Borrows(id, user_id, book_id, borrowDate, returnDate);
                                    borrowsDAO.Save(borrows);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid datatype");
                                    break;
                                }
                            }
                            else if (answer2 == "insert")
                            {
                                Console.WriteLine("user_id: ");
                                string answer4 = Console.ReadLine();
                                Console.WriteLine("book_id: ");
                                string answer5 = Console.ReadLine();
                                Console.WriteLine("borrow date (dd/mm/yyyy): ");
                                string answer6 = Console.ReadLine();
                                Console.WriteLine("return date (dd/mm/yyyy): ");
                                string answer7 = Console.ReadLine();

                                if (int.TryParse(answer4, out int user_id) && int.TryParse(answer5, out int book_id) && DateTime.TryParse(answer6, out DateTime borrowDate) && DateTime.TryParse(answer7, out DateTime returnDate))
                                {
                                    Loan borrows = new Borrows(user_id, book_id, borrowDate, returnDate);
                                    borrowsDAO.Save(borrows);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid datatype");
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("unknown command, you can type 'help' to show all commands");
                                break;
                            }
                            break;

                        case "help":
                            Console.WriteLine(WriteCommands());
                            repeat2 = false;
                            break;

                        case "exit":
                            repeat = false;
                            break;

                        default:
                            Console.WriteLine("unknown command, you can type 'help' to show all commands");
                            repeat2 = false;
                            break;
                    }
                }
            } while (repeat);
        }

        /// <summary>
        /// Vypise vsechny dostupne prikazy z prvniho stupne
        /// </summary>
        /// <returns>prikazy</returns>
        public string WriteCommands()
        {
            string output = "\n Commands: \n";
            for (int i = 0; i < commands.Count; i++)
            {
                output += $"{i+1}) {commands[i]} \n";
            }
            return output;
        }

        /// <summary>
        /// Vypise vsechny dostupne prikazy z druheho stupne
        /// </summary>
        /// <returns>prikazy</returns>
        public string WriteTableCommands()
        {
            string output = "\n Commands: \n";
            for (int i = 0; i < tableCommands.Count; i++)
            {
                output += $"{i+1}) {tableCommands[i]} \n";
            }
            return output;
        }
    }
}
