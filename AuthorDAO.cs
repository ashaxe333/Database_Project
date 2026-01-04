using System.Data.SqlClient;

namespace Projekt
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
                SqlConnection conn = DatabaseSingleton.GetInstance();

                Console.WriteLine("deleting author");
                using (SqlCommand command = new SqlCommand("DELETE FROM authors WHERE id = @id", conn))
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
        /// Inserts or updates author based on id
        /// </summary>
        /// <param name="author"> author to insert or update </param>
        public void Save(Author author)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            if (author.Id < 1)
            {
                Console.WriteLine("inserting author");
                using (command = new SqlCommand("INSERT INTO authors ([name]) VALUES (@name)", conn))
                {
                    command.Parameters.Add(new SqlParameter("@name", author.Name));
                    command.ExecuteNonQuery();

                    command.CommandText = "Select @@Identity";
                    author.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                try
                {
                    Console.WriteLine("updating author");
                    using (command = new SqlCommand("UPDATE authors SET [name] = @name WHERE id = @id", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@id", author.Id));
                        command.Parameters.Add(new SqlParameter("@name", author.Name));
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
        /// Gets all authors from the table
        /// </summary>
        public void GetAll()
        {
            try
            {
                SqlConnection conn = DatabaseSingleton.GetInstance();

                Console.WriteLine("get all authors");
                using (SqlCommand command = new SqlCommand("select * FROM authors", conn))
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
        /// Gets author from the table by id
        /// </summary>
        /// <param name="id"> author id </param>
        public void GetById(int id)
        {
            try
            {
                Console.WriteLine("getting author");
                SqlConnection conn = DatabaseSingleton.GetInstance();

                using (SqlCommand command = new SqlCommand("SELECT * FROM authors WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"author with id {id} does not exist");
            }
        }

        public override string? ToString()
        {
            return "AuthorTable";
        }
    }
}
