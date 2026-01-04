using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Book_AuthorsDAO : IDAO<Book_Authors>
    {
        /// <summary>
        /// Deletes book_authors from table ba id
        /// </summary>
        /// <param name="id"> book_author id </param>
        public void Delete(int id)
        {
            try
            {
                MySqlConnection conn = DatabaseSingleton.GetInstance();

                Console.WriteLine("deleting book_authors");
                using (MySqlCommand command = new MySqlCommand("DELETE FROM book_authors WHERE id = @id", conn))
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
        /// Inserts or updates book_author based on id
        /// </summary>
        /// <param name="author"> book_author to insert or update </param>
        public void Save(Book_Authors book_authors)
        {
            MySqlConnection conn = DatabaseSingleton.GetInstance();

            MySqlCommand command = null;

            if (book_authors.Id < 1)
            {
                Console.WriteLine("inserting book_authors");
                using (command = new MySqlCommand("INSERT INTO book_authors (book_id, author_id) VALUES (@book_id, @author_id)", conn))
                {
                    command.Parameters.Add(new MySqlParameter("@book_id", book_authors.Book_id));
                    command.Parameters.Add(new MySqlParameter("@author_id", book_authors.Author_id));
                    command.ExecuteNonQuery();

                    command.CommandText = "Select @@Identity";
                    book_authors.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                try
                {
                    Console.WriteLine("updating book_authors");
                    using (command = new MySqlCommand("UPDATE book_authors SET book_id = @book_id, author_id = @author_id WHERE id = @id", conn))
                    {
                        command.Parameters.Add(new MySqlParameter("@id", book_authors.Id));
                        command.Parameters.Add(new MySqlParameter("@book_id", book_authors.Book_id));
                        command.Parameters.Add(new MySqlParameter("@author_id", book_authors.Author_id));
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
        /// Gets all payments
        /// </summary>
        /// <returns> List of Payments </returns>
        public List<Book_Authors> GetAll()
        {
            List<Book_Authors> result = new List<Book_Authors>();

            try
            {
                MySqlConnection conn = DatabaseSingleton.GetInstance();

                Console.WriteLine("get all book_authors");
                using (MySqlCommand command = new MySqlCommand("SELECT id, book_id, author_id FROM book_authors", conn)) //1 connection, 1 reader v jeden moment
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Book_Authors(reader.GetInt32("id"), reader.GetInt32("book_id"), reader.GetInt32("author_id")));
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
        /// Gets payment from the table by id
        /// </summary>
        /// <param name="id"> payment id </param>
        /// <returns> Payment </returns>
        public Book_Authors? GetById(int id)
        {
            Book_Authors? result = null;
            try
            {
                Console.WriteLine("getting book_authors");
                MySqlConnection conn = DatabaseSingleton.GetInstance();

                using (MySqlCommand command = new MySqlCommand("SELECT * FROM book_authors WHERE id = @id", conn))
                {
                    command.Parameters.Add(new MySqlParameter("@id", id));

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) result = new Book_Authors(id, reader.GetInt32("book_id"), reader.GetInt32("author_id"));
                        else Console.WriteLine($"book_authors with id {id} does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public override string? ToString()
        {
            return "Book_Authors Table";
        }
    }
}
