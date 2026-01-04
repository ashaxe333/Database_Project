using MySql.Data.MySqlClient;
using System.Globalization;

namespace Projekt
{
    public class BookDAO : IDAO<Book>
    {
        /// <summary>
        /// Deletes book from table by id
        /// </summary>
        /// <param name="id"> book id </param>
        public void Delete(int id)
        {
            try {
                Console.WriteLine("deleting book");
                MySqlConnection conn = DatabaseSingleton.GetInstance();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM books WHERE id = @id", conn))
                {
                    command.Parameters.Add(new MySqlParameter("@id", id));
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Currently used as foreign key -> cannot be deleted");
            }
}

        /// <summary>
        /// Inserts or updates book based on id
        /// </summary>
        /// <param name="book"> book to insert or update </param>
        public void Save(Book book)
        {

            MySqlConnection conn = DatabaseSingleton.GetInstance();

            MySqlCommand command = null;


            if (book.Id < 1)
            {
                try
                {
                    Console.WriteLine("inserting book");
                    using (command = new MySqlCommand("INSERT INTO books (title, published_year, available) VALUES (@title, @published_year, @isAvailable)", conn))
                    {
                        command.Parameters.Add(new MySqlParameter("@title", book.Title));
                        command.Parameters.Add(new MySqlParameter("@published_year", book.Published_year));
                        command.Parameters.Add(new MySqlParameter("@isAvailable", book.IsAvailable));
                        command.ExecuteNonQuery();

                        command.CommandText = "Select LAST_INSERT_ID()";
                        book.Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("One of foreign keys cannot be added -> object doesnt exist");
                }
            }
            else
            {
                try
                {
                    Console.WriteLine("updating book");
                    using (command = new MySqlCommand("UPDATE books SET title = @title, published_year = @published_yeare, available = @isAvailable WHERE id = @id", conn))
                    {
                        command.Parameters.Add(new MySqlParameter("@id", book.Id)); 
                        command.Parameters.Add(new MySqlParameter("@title", book.Title));
                        command.Parameters.Add(new MySqlParameter("@published_year", book.Published_year));
                        command.Parameters.Add(new MySqlParameter("@isAvailable", book.IsAvailable));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Selected id doesnt exist or one of foreign keys cannot be added -> object doesnt exist");
                }
            }
        }

        /// <summary>
        /// Gets all books
        /// </summary>
        /// <returns> List of Books </returns>
        public List<Book> GetAll()
        {
            List<Book> result = new List<Book>();

            try
            {
                MySqlConnection conn = DatabaseSingleton.GetInstance();

                Console.WriteLine("get all books");
                using (MySqlCommand command = new MySqlCommand("SELECT id, title, published_year, available  FROM books", conn)) //1 connection, 1 reader v jeden moment
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            string title = reader.GetString("title");
                            int published_year = reader.GetInt32("book_id");
                            bool available = reader.GetBoolean("available");

                            result.Add(new Book(id, title, published_year, available));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Gets book from the table by id
        /// </summary>
        /// <param name="id"> book id </param>
        /// <returns> Book object </returns>
        public Book? GetById(int id)
        {
            Book? result = null;
            try
            {
                Console.WriteLine($"getting book with id {id}");
                MySqlConnection conn = DatabaseSingleton.GetInstance();

                using (MySqlCommand command = new MySqlCommand("SELECT title, published_year, available FROM books WHERE id = @id", conn))
                {
                    command.Parameters.Add(new MySqlParameter("@id", id));

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string title = reader.GetString("title");
                            int published_year = reader.GetInt32("book_id");
                            bool available = reader.GetBoolean("available");

                            result = new Book(id, title, published_year, available);
                        }
                        else Console.WriteLine($"Book with id {id} does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Imports data from CSV file
        /// </summary>
        /// <returns> Messages </returns>
        public string importCSV()
        {
            try
            {
                string line;
                using (StreamReader reader = new StreamReader("knihy.csv"))
                {
                    bool help = false;
                    line = reader.ReadLine();

                    while (line != null)
                    {
                        line.Trim();
                        var parts = line.Split(',');

                        if (int.TryParse(parts[1], out int published_year) && bool.TryParse(parts[2], out bool isAvailable))
                        {
                            Save(new Book(parts[0], published_year, isAvailable));
                        }
                        else
                        {
                            help = true;
                            return "Invalid datatype";
                        }
                        line = reader.ReadLine();
                    }
                    if (!help) return "Import done";
                    else return "Invalid datatype";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Failed to load file";
            }
        }

        public override string? ToString()
        {
            return "Books Table";
        }
    }
}
