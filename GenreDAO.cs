using System.Data.SqlClient;

namespace Projekt
{
    public class GenreDAO : IDAO<Genre>
    {
        /// <summary>
        /// Smaze zanr podle id
        /// </summary>
        /// <param name="id">id zanru, ktery se bude mazat</param>
        public void Delete(int id)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Genre WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Na zaklade ID provede insert nebo update zadaneho zanru
        /// </summary>
        /// <param name="user">zanr, ktery se pripise nebo aktualizuje v databazi</param>
        public void Save(Genre genre)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            if (genre.Id < 1)
            {
                using (command = new SqlCommand("INSERT INTO Genre ([name]) VALUES (@name)", conn))
                {
                    command.Parameters.Add(new SqlParameter("@name", genre.Name));
                    command.ExecuteNonQuery();

                    command.CommandText = "Select @@Identity";
                    genre.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE Genre SET [name] = @name WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", genre.Id));
                    command.Parameters.Add(new SqlParameter("@name", genre.Name));
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Imports data from CSV file
        /// </summary>
        /// <returns>Messages</returns>
        public string importCSV()
        {
            try
            {
                string line;
                using (StreamReader reader = new StreamReader("zanry.csv")) 
                { 
                    line = reader.ReadLine();

                    while (line != null)
                    {
                        Save(new Genre(line));
                        line = reader.ReadLine();
                    }
                    reader.Close();
                    return "Import done";
                }   
            }
            catch (Exception ex)
            {
                return "Failed to load file";
            }
        }

        public override string? ToString()
        {
            return "GenreTable";
        }
    }
}
