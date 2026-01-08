using MySql.Data.MySqlClient;
using WindowsFormsApp1.Database;
using WindowsFormsApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;

namespace WindowsFormsApp1.DAO
{
    public class Book_AuthorsDAO : IDAO<Book_Authors>
    {
        /// <summary>
        /// Deletes book_authors from table ba id
        /// </summary>
        /// <param name="id"> book_author id </param>
        public void Delete(int id)
        {
            MySqlConnection conn = DatabaseSingleton.GetInstance();

            using (MySqlCommand command = new MySqlCommand("DELETE FROM book_authors WHERE id = @id", conn))
            {
                command.Parameters.Add(new MySqlParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Inserts or updates book_author based on id
        /// </summary>
        /// <param name="book_authors"> book_author to insert or update </param>
        public void Save(Book_Authors book_authors, MySqlTransaction transaction)
        {
            MySqlConnection conn = DatabaseSingleton.GetInstance();

            MySqlCommand command = null;

            if (book_authors.Id < 1)
            {
                if (transaction != null)
                    command = new MySqlCommand("INSERT INTO book_authors (book_id, author_id) VALUES (@book_id, @author_id)", transaction.Connection, transaction);
                else
                    command = new MySqlCommand("INSERT INTO book_authors (book_id, author_id) VALUES (@book_id, @author_id)", conn);

                using (command)
                {
                    command.Parameters.Add(new MySqlParameter("@book_id", book_authors.Book_id));
                    command.Parameters.Add(new MySqlParameter("@author_id", book_authors.Author_id));
                    command.ExecuteNonQuery();

                    command.CommandText = "Select LAST_INSERT_ID()";
                    book_authors.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                if (transaction != null)
                    command = new MySqlCommand("UPDATE book_authors SET book_id = @book_id, author_id = @author_id WHERE id = @id", transaction.Connection, transaction);
                else
                    command = new MySqlCommand("UPDATE book_authors SET book_id = @book_id, author_id = @author_id WHERE id = @id", conn);

                using (command)
                {
                    command.Parameters.Add(new MySqlParameter("@id", book_authors.Id));
                    command.Parameters.Add(new MySqlParameter("@book_id", book_authors.Book_id));
                    command.Parameters.Add(new MySqlParameter("@author_id", book_authors.Author_id));
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Gets all book_authors
        /// </summary>
        /// <returns> List of Book_Authors </returns>
        public List<Book_Authors> GetAll()
        {
            List<Book_Authors> result = new List<Book_Authors>();
            MySqlConnection conn = DatabaseSingleton.GetInstance();

            using (MySqlCommand command = new MySqlCommand("SELECT id, book_id, author_id FROM book_authors", conn)) //1 connection, 1 reader v jeden moment
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        int book_id = reader.GetInt32("book_id");
                        int author_id = reader.GetInt32("author_id");

                        result.Add(new Book_Authors(id, book_id, author_id));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets book_Authors from the table by id
        /// </summary>
        /// <param name="id"> book_Authors id </param>
        /// <returns> Book_Authors object </returns>
        public Book_Authors GetById(int id)
        {
            Book_Authors result = null;
            MySqlConnection conn = DatabaseSingleton.GetInstance();

            using (MySqlCommand command = new MySqlCommand("SELECT book_id, author_id FROM book_authors WHERE id = @id", conn))
            {
                command.Parameters.Add(new MySqlParameter("@id", id));

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int book_id = reader.GetInt32("book_id");
                        int author_id = reader.GetInt32("author_id");

                        result = new Book_Authors(id, book_id, author_id);
                    }
                    else throw new Exception($"book_authors with id {id} does not exist");
                }
            }

            return result;
        }

        public override string ToString()
        {
            return "Book_Authors Table";
        }
    }
}
