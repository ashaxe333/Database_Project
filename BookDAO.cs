using System.Data.SqlClient;
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
                SqlConnection conn = DatabaseSingleton.GetInstance();

                using (SqlCommand command = new SqlCommand("DELETE FROM books WHERE id = @id", conn))
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
        /// Inserts or updates book based on id
        /// </summary>
        /// <param name="book"> book to insert or update </param>
        public void Save(Book book)
        {

            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;


            if (book.Id < 1)
            {
                try
                {
                    Console.WriteLine("inserting book");
                    using (command = new SqlCommand("INSERT INTO books (title, published_year, isAvailable) VALUES (@title, @published_year, @isAvailablee)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@title", book.Title));
                        command.Parameters.Add(new SqlParameter("@published_year", book.Published_year));
                        command.Parameters.Add(new SqlParameter("@isAvailable", book.IsAvailable));
                        command.ExecuteNonQuery();

                        command.CommandText = "Select @@Identity";
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
                    using (command = new SqlCommand("UPDATE books SET title = @title, published_year = @published_yeare, isAvailable = @isAvailable WHERE id = @id", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@id", book.Id)); 
                        command.Parameters.Add(new SqlParameter("@title", book.Title));
                        command.Parameters.Add(new SqlParameter("@published_year", book.Published_year));
                        command.Parameters.Add(new SqlParameter("@isAvailable", book.IsAvailable));
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
        /// Gets all book from the table
        /// </summary>
        public void GetAll()
        {
            try
            {
                SqlConnection conn = DatabaseSingleton.GetInstance();

                Console.WriteLine("get all authors");
                using (SqlCommand command = new SqlCommand("select * FROM books", conn))
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
        /// Gets book from the table by id
        /// </summary>
        /// <param name="id"> book id </param>
        public void GetById(int id)
        {
            try
            {
                Console.WriteLine("getting book");
                SqlConnection conn = DatabaseSingleton.GetInstance();

                using (SqlCommand command = new SqlCommand("SELECT * FROM books WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"book with id {id} does not exist");
            }
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
            return "BookTable";
        }
    }
}
