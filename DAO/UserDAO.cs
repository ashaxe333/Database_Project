using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using WindowsFormsApp1.Database;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.DAO
{
    public class UserDAO : IDAO<User>
    {
        /// <summary>
        /// Delets user from table by id
        /// </summary>
        /// <param name="id"> user id </param>
        public void Delete(int id)
        {
            try
            {
                MySqlConnection conn = DatabaseSingleton.GetInstance();

                Console.WriteLine("deleting user");
                using (MySqlCommand command = new MySqlCommand("DELETE FROM users WHERE id = @id", conn))
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
        /// Inserts or updates user based on id
        /// </summary>
        /// <param name="user"> user to update or insert </param>
        public void Save(User user)
        {
            MySqlConnection conn = DatabaseSingleton.GetInstance();

            MySqlCommand command = null;

            if (user.Id < 1)
            {
                try
                {
                    Console.WriteLine("inserting user");
                    using (command = new MySqlCommand("INSERT INTO users (name, email, is_active, created_at) VALUES (@name, @email, @is_active, @created_at)", conn))
                    {
                        command.Parameters.Add(new MySqlParameter("@name", user.Name));
                        command.Parameters.Add(new MySqlParameter("@email", user.Email));
                        command.Parameters.Add(new MySqlParameter("@is_active", user.Is_active));
                        command.Parameters.Add(new MySqlParameter("@created_at", user.Created_at));
                        command.ExecuteNonQuery();

                        command.CommandText = "Select LAST_INSERT_ID()";
                        user.Id = Convert.ToInt32(command.ExecuteScalar());
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
                    Console.WriteLine("updating user");
                    using (command = new MySqlCommand("UPDATE users SET name = @name, email = @email, is_active = @is_active, created_at = @created_at WHERE id = @id", conn))
                    {
                        command.Parameters.Add(new MySqlParameter("@id", user.Id));
                        command.Parameters.Add(new MySqlParameter("@name", user.Name));
                        command.Parameters.Add(new MySqlParameter("@email", user.Email));
                        command.Parameters.Add(new MySqlParameter("@is_active", user.Is_active));
                        command.Parameters.Add(new MySqlParameter("@created_at", user.Created_at));
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
        /// Gets all users
        /// </summary>
        /// <returns> List of Users </returns>
        public List<User> GetAll()
        {
            List<User> result = new List<User>();

            try
            {
                MySqlConnection conn = DatabaseSingleton.GetInstance();

                Console.WriteLine("get all users");
                using (MySqlCommand command = new MySqlCommand("SELECT id, name, email, is_active, created_at FROM users", conn)) //1 connection, 1 reader v jeden moment
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            string name = reader.GetString("name");
                            string email = reader.GetString("email");
                            bool is_active = reader.GetBoolean("is_active");
                            DateTime created_at = reader.GetDateTime("created_at");

                            result.Add(new User(id, name, email, is_active, created_at));
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
        /// Gets user from the table by id
        /// </summary>
        /// <param name="id"> user id </param>
        /// <returns> User object </returns>
        public User GetById(int id)
        {
            User result = null;
            try
            {
                Console.WriteLine("getting user");
                MySqlConnection conn = DatabaseSingleton.GetInstance();

                using (MySqlCommand command = new MySqlCommand("SELECT name, email, is_active, created_at FROM users WHERE id = @id", conn))
                {
                    command.Parameters.Add(new MySqlParameter("@id", id));

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader.GetString("name");
                            string email = reader.GetString("email");
                            bool is_active = reader.GetBoolean("is_active");
                            DateTime created_at = reader.GetDateTime("created_at");

                            result = new User(id, name, email, is_active, created_at);
                        }
                        else Console.WriteLine($"User with id {id} does not exist");
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
            return "Users Table";
        }
    }
}
