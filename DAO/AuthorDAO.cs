using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using WindowsFormsApp1.Database;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.DAO
{
    public class AuthorDAO : IDAO<Author>
    {
        /// <summary>
        /// Deletes author from table by id
        /// </summary>
        /// <param name="id"> author id </param>
        public void Delete(int id)
        {
            MySqlConnection conn = DatabaseSingleton.GetInstance();

            using (MySqlCommand command = new MySqlCommand("DELETE FROM authors WHERE id = @id", conn))
            {
                command.Parameters.Add(new MySqlParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Inserts or updates author based on id
        /// </summary>
        /// <param name="author"> author to insert or update </param>
        public void Save(Author author, MySqlTransaction transaction)
        {
            MySqlConnection conn = DatabaseSingleton.GetInstance();
            MySqlCommand command = null;

            if (author.Id < 1)
            {
                if(transaction != null) 
                    command = new MySqlCommand("INSERT INTO authors (name) VALUES (@name)", transaction.Connection, transaction);
                else 
                    command = new MySqlCommand("INSERT INTO authors (name) VALUES (@name)", conn);

                using (command)
                {
                    command.Parameters.Add(new MySqlParameter("@name", author.Name));
                    command.ExecuteNonQuery();

                    command.CommandText = "Select LAST_INSERT_ID()";
                    author.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                if(transaction != null)
                    command = new MySqlCommand("UPDATE authors SET name = @name WHERE id = @id", transaction.Connection, transaction);
                else
                    command = new MySqlCommand("UPDATE authors SET name = @name WHERE id = @id", conn);

                using (command)
                {
                    command.Parameters.Add(new MySqlParameter("@id", author.Id));
                    command.Parameters.Add(new MySqlParameter("@name", author.Name));
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Gets all authors
        /// </summary>
        /// <returns> List of Authors </returns>
        public List<Author> GetAll()
        {
            List<Author> result = new List<Author>();
            MySqlConnection conn = DatabaseSingleton.GetInstance();

            using (MySqlCommand command = new MySqlCommand("SELECT id, name FROM authors", conn)) //1 connection, 1 reader v jeden moment
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string name = reader.GetString("name");

                        result.Add(new Author(id, name));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets author from the table by id
        /// </summary>
        /// <param name="id"> author id </param>
        /// <returns> Author object </returns>
        public Author GetById(int id)
        {
            Author result = null;
            MySqlConnection conn = DatabaseSingleton.GetInstance();

            using (MySqlCommand command = new MySqlCommand("SELECT * FROM authors WHERE id = @id", conn))
            {
                command.Parameters.Add(new MySqlParameter("@id", id));

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string name = reader.GetString("name");

                        result = new Author(id, name);
                    }
                    else throw new Exception($"Author with id {id} does not exist");
                }
            }

            return result;
        }

        /// <summary>
        /// Imports data from CSV file
        /// </summary>
        /// <returns>Messages</returns>
        public void importCSV(string path)
        {
            string line;
            using (StreamReader reader = new StreamReader(path))
            {
                line = reader.ReadLine();

                while (line != null)
                {
                    //prevents to importing wrong data
                    if (line.Contains(","))
                    {
                        throw new Exception("Name cannot contain separator (,)");
                    }

                    Save(new Author(line), null);
                    line = reader.ReadLine();
                }
                reader.Close();
            }
        }

        public override string ToString()
        {
            return "Authors Table";
        }
    }
}
