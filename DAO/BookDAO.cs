using MySql.Data.MySqlClient;
using WindowsFormsApp1.Database;
using WindowsFormsApp1.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace WindowsFormsApp1.DAO
{
    public class BookDAO : IDAO<Book>
    {
        /// <summary>
        /// Deletes book from table by id
        /// </summary>
        /// <param name="id"> book id </param>
        public void Delete(int id)
        {
            MySqlConnection conn = DatabaseSingleton.GetInstance();

            using (MySqlCommand command = new MySqlCommand("DELETE FROM books WHERE id = @id", conn))
            {
                command.Parameters.Add(new MySqlParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Inserts or updates book based on id
        /// </summary>
        /// <param name="book"> book to insert or update </param>
        public void Save(Book book, MySqlTransaction transaction)
        {

            MySqlConnection conn = DatabaseSingleton.GetInstance();
            MySqlCommand command = null;

            if (book.Id < 1)
            {
                if (transaction != null)
                    command = new MySqlCommand("INSERT INTO books (title, published_year, available) VALUES (@title, @published_year, @isAvailable)", transaction.Connection, transaction);
                else
                    command = new MySqlCommand("INSERT INTO books (title, published_year, available) VALUES (@title, @published_year, @isAvailable)", conn);

                using (command)
                {
                    command.Parameters.Add(new MySqlParameter("@title", book.Title));
                    command.Parameters.Add(new MySqlParameter("@published_year", book.Published_year));
                    command.Parameters.Add(new MySqlParameter("@isAvailable", book.IsAvailable));
                    command.ExecuteNonQuery();

                    command.CommandText = "Select LAST_INSERT_ID()";
                    book.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                if (transaction != null)
                    command = new MySqlCommand("UPDATE books SET title = @title, published_year = @published_year, available = @isAvailable WHERE id = @id", transaction.Connection, transaction);
                else 
                    command = new MySqlCommand("UPDATE books SET title = @title, published_year = @published_year, available = @isAvailable WHERE id = @id", conn);

                using (command)
                {
                    command.Parameters.Add(new MySqlParameter("@id", book.Id));
                    command.Parameters.Add(new MySqlParameter("@title", book.Title));
                    command.Parameters.Add(new MySqlParameter("@published_year", book.Published_year));
                    command.Parameters.Add(new MySqlParameter("@isAvailable", book.IsAvailable));
                    command.ExecuteNonQuery();
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
            MySqlConnection conn = DatabaseSingleton.GetInstance();

            using (MySqlCommand command = new MySqlCommand("SELECT id, title, published_year, available  FROM books", conn)) //1 connection, 1 reader v jeden moment
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string title = reader.GetString("title");
                        int? published_year = reader.IsDBNull(reader.GetOrdinal("published_year")) ? (int?)null : reader.GetInt32("published_year");
                        bool available = reader.GetBoolean("available");

                        result.Add(new Book(id, title, published_year, available));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets book from the table by id
        /// </summary>
        /// <param name="id"> book id </param>
        /// <returns> Book object </returns>
        public Book GetById(int id)
        {
            Book result = null;
            MySqlConnection conn = DatabaseSingleton.GetInstance();

            using (MySqlCommand command = new MySqlCommand("SELECT title, published_year, available FROM books WHERE id = @id", conn))
            {
                command.Parameters.Add(new MySqlParameter("@id", id));

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string title = reader.GetString("title");
                        int? published_year = reader.IsDBNull(reader.GetOrdinal("published_year")) ? (int?)null : reader.GetInt32("published_year");
                        bool available = reader.GetBoolean("available");

                        result = new Book(id, title, published_year, available);
                    }
                    else throw new Exception($"Book with id {id} does not exist");
                }
            }

            return result;
        }

        /// <summary>
        /// Imports data from CSV file
        /// </summary>
        /// <returns> Messages </returns>
        public void importCSV(string path)
        {
            string line;
            using (StreamReader reader = new StreamReader(path))
            {
                bool help = false;
                line = reader.ReadLine();

                while (line != null)
                {
                    line.Trim();
                    var parts = line.Split(',');

                    if (int.TryParse(parts[1], out int published_year) && bool.TryParse(parts[2], out bool isAvailable))
                    {
                        Save(new Book(parts[0], published_year, isAvailable), null);
                    }
                    else
                    {
                        help = true;
                    }
                    line = reader.ReadLine();
                }
            }
        }

        public override string ToString()
        {
            return "Books Table";
        }
    }
}
