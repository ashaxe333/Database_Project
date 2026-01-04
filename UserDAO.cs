using System.Data.SqlClient;

namespace Projekt
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
                SqlConnection conn = DatabaseSingleton.GetInstance();

                Console.WriteLine("deleting user");
                using (SqlCommand command = new SqlCommand("DELETE FROM users WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));
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
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            if (user.Id < 1)
            {
                try
                {
                    Console.WriteLine("inserting user");
                    using (command = new SqlCommand("INSERT INTO users ([name], email, is_active, created_at) VALUES (@name, @email, @is_active, @created_at)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@name", user.Name));
                        command.Parameters.Add(new SqlParameter("@email", user.Email));
                        command.Parameters.Add(new SqlParameter("@is_active", user.Is_active));
                        command.Parameters.Add(new SqlParameter("@created_at", user.Created_at));
                        command.ExecuteNonQuery();

                        command.CommandText = "Select @@Identity";
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
                    using (command = new SqlCommand("UPDATE users SET [name] = @name, email = @email, is_active = @is_active, created_at = @created_at WHERE id = @id", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@id", user.Id));
                        command.Parameters.Add(new SqlParameter("@name", user.Name));
                        command.Parameters.Add(new SqlParameter("@email", user.Email));
                        command.Parameters.Add(new SqlParameter("@is_active", user.Is_active));
                        command.Parameters.Add(new SqlParameter("@created_at", user.Created_at));
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
        /// Gets all users from the table
        /// </summary>
        public void GetAll()
        {
            try
            {
                SqlConnection conn = DatabaseSingleton.GetInstance();

                Console.WriteLine("get all users");
                using (SqlCommand command = new SqlCommand("select * FROM users", conn))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// Gets user from the table by id
        /// </summary>
        /// <param name="id"> user id </param>
        public void GetById(int id)
        {
            try
            {
                Console.WriteLine("getting user");
                SqlConnection conn = DatabaseSingleton.GetInstance();

                using (SqlCommand command = new SqlCommand("SELECT * FROM users WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"user with id {id} does not exist");
            }
        }

        public override string? ToString()
        {
            return "UserTable";
        }
    }
}
