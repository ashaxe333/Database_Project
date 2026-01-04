using System.Data.SqlClient;

namespace Projekt
{
    public class LoanDAO : IDAO<Loan>
    {
        /// <summary>
        /// Deletes loan from table by id
        /// </summary>
        /// <param name="id"> loan id </param>
        public void Delete(int id)
        {
            try
            {
                Console.WriteLine("deleting loans");
                SqlConnection conn = DatabaseSingleton.GetInstance();

                using (SqlCommand command = new SqlCommand("DELETE FROM loans WHERE id = @id", conn))
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
        /// Innserts or updates loan based on id
        /// </summary>
        /// <param name="loan"> lloan to insert or update </param>
        public void Save(Loan loan)
        {

            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            if (loan.Id < 1)
            {
                try
                {
                    Console.WriteLine("inserting loans");
                    using (command = new SqlCommand("INSERT INTO loans ([user_id], book_id, loanDate, returnDate) VALUES (@user_id, @book_id, @loanDate, @returnDate)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@user_id", loan.User_id));
                        command.Parameters.Add(new SqlParameter("@book_id", loan.Book_id));
                        command.Parameters.Add(new SqlParameter("@loanDate", loan.LoanDate));
                        command.Parameters.Add(new SqlParameter("@returnDate", loan.ReturnDate));
                        command.ExecuteNonQuery();

                        command.CommandText = "Select @@Identity";
                        loan.Id = Convert.ToInt32(command.ExecuteScalar());
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
                    Console.WriteLine("updating loans");
                    using (command = new SqlCommand("UPDATE loans SET user_id = @user_id, book_id = @book_id, loan_date = @loanDate, return_date = @returnDate WHERE id = @id", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@id", loan.Id));
                        command.Parameters.Add(new SqlParameter("@user_id", loan.User_id));
                        command.Parameters.Add(new SqlParameter("@book_id", loan.Book_id));
                        command.Parameters.Add(new SqlParameter("@loanDate", loan.LoanDate));
                        command.Parameters.Add(new SqlParameter("@returnDate", loan.ReturnDate));
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
        /// Gets all loans from the table
        /// </summary>
        public void GetAll()
        {
            try
            {
                SqlConnection conn = DatabaseSingleton.GetInstance();

                Console.WriteLine("get all loans");
                using (SqlCommand command = new SqlCommand("select * FROM loans", conn))
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
        /// Gets loan from the table by id
        /// </summary>
        /// <param name="id"> loan id </param>
        public void GetById(int id)
        {
            try
            {
                Console.WriteLine("getting loan");
                SqlConnection conn = DatabaseSingleton.GetInstance();

                using (SqlCommand command = new SqlCommand("SELECT * FROM loans WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"loan with id {id} does not exist");
            }
        }

        public override string? ToString()
        {
            return "LoansTable";
        }
    }
}
