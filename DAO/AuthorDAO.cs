using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
            try
            {
                MySqlConnection conn = DatabaseSingleton.GetInstance();
                Console.WriteLine("deleting author");
                using (MySqlCommand command = new MySqlCommand("DELETE FROM authors WHERE id = @id", conn))
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
        /// Inserts or updates author based on id
        /// </summary>
        /// <param name="author"> author to insert or update </param>
        public void Save(Author author)
        {
            MySqlConnection conn = DatabaseSingleton.GetInstance();

            MySqlCommand command = null;

            if (author.Id < 1)
            {
                Console.WriteLine("inserting author");
                using (command = new MySqlCommand("INSERT INTO authors (name) VALUES (@name)", conn))
                {
                    command.Parameters.Add(new MySqlParameter("@name", author.Name));
                    command.ExecuteNonQuery();

                    command.CommandText = "Select LAST_INSERT_ID()";
                    author.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                try
                {
                    Console.WriteLine("updating author");
                    using (command = new MySqlCommand("UPDATE authors SET name = @name WHERE id = @id", conn))
                    {
                        command.Parameters.Add(new MySqlParameter("@id", author.Id));
                        command.Parameters.Add(new MySqlParameter("@name", author.Name));
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
        /// Gets all authors
        /// </summary>
        /// <returns> List of Authors </returns>
        public List<Author> GetAll()
        {
            List<Author> result = new List<Author>();
            
            try
            {
                MySqlConnection conn = DatabaseSingleton.GetInstance();

                Console.WriteLine("get all authors");
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            try
            {
                Console.WriteLine("getting author");
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
                        else Console.WriteLine($"Author with id {id} does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public override string ToString()
        {
            return "Authors Table";
        }
    }
}
