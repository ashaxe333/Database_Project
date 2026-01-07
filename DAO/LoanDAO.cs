using MySql.Data.MySqlClient;
using WindowsFormsApp1.Database;
using WindowsFormsApp1.Models;
using System.Data;
using System;
using System.Collections.Generic;

namespace WindowsFormsApp1.DAO
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
                MySqlConnection conn = DatabaseSingleton.GetInstance();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM loans WHERE id = @id", conn))
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
        /// Innserts or updates loan based on id
        /// </summary>
        /// <param name="loan"> lloan to insert or update </param>
        public void Save(Loan loan)
        {

            MySqlConnection conn = DatabaseSingleton.GetInstance();

            MySqlCommand command = null;

            if (loan.Id < 1)
            {
                try
                {
                    Console.WriteLine("inserting loans");
                    using (command = new MySqlCommand("INSERT INTO loans (user_id, book_id, loan_date, return_date, status) VALUES (@user_id, @book_id, @loan_date, @return_date, @status)", conn))
                    {
                        command.Parameters.Add(new MySqlParameter("@user_id", loan.User_id));
                        command.Parameters.Add(new MySqlParameter("@book_id", loan.Book_id));
                        command.Parameters.Add(new MySqlParameter("@loan_date", loan.Loan_date));
                        command.Parameters.Add(new MySqlParameter("@return_date", loan.Return_date));
                        command.Parameters.Add(new MySqlParameter("@status", loan.Status.ToString()));
                        command.ExecuteNonQuery();

                        command.CommandText = "Select LAST_INSERT_ID()";
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
                    using (command = new MySqlCommand("UPDATE loans SET user_id = @user_id, book_id = @book_id, loan_date = @loan_date, return_date = @return_date, status = @status WHERE id = @id", conn))
                    {
                        command.Parameters.Add(new MySqlParameter("@id", loan.Id));
                        command.Parameters.Add(new MySqlParameter("@user_id", loan.User_id));
                        command.Parameters.Add(new MySqlParameter("@book_id", loan.Book_id));
                        command.Parameters.Add(new MySqlParameter("@loan_date", loan.Loan_date));
                        command.Parameters.Add(new MySqlParameter("@return_date", loan.Return_date));
                        command.Parameters.Add(new MySqlParameter("@status", loan.Status.ToString()));
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
        /// Gets all loans
        /// </summary>
        /// <returns> List of Loans </returns>
        public List<Loan> GetAll()
        {
            List<Loan> result = new List<Loan>();

            try
            {
                MySqlConnection conn = DatabaseSingleton.GetInstance();

                Console.WriteLine("get all loans");
                using (MySqlCommand command = new MySqlCommand("SELECT id, user_id, book_id, loan_date, return_date, status FROM loans", conn)) //1 connection, 1 reader v jeden moment
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            int user_id = reader.GetInt32("user_id");
                            int book_id = reader.GetInt32("book_id");
                            DateTime loan_date = reader.GetDateTime("loan_date");
                            DateTime? return_date = reader.IsDBNull(reader.GetOrdinal("return_date")) ? (DateTime?)null : reader.GetDateTime("return_date");
                            LoanStatus status = (LoanStatus)Enum.Parse(typeof(LoanStatus), reader.GetString("status"));

                            result.Add(new Loan(id, user_id, book_id, loan_date, return_date, status));
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
        /// Gets loan from the table by id
        /// </summary>
        /// <param name="id"> loan id </param>
        /// <returns> Loan object </returns>
        public Loan GetById(int id)
        {
            Loan result = null;
            try
            {
                Console.WriteLine("getting loan");
                MySqlConnection conn = DatabaseSingleton.GetInstance();

                using (MySqlCommand command = new MySqlCommand("SELECT user_id, book_id, loan_date, return_date, status FROM loans WHERE id = @id", conn))
                {
                    command.Parameters.Add(new MySqlParameter("@id", id));

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int user_id = reader.GetInt32("user_id");
                            int book_id = reader.GetInt32("book_id");
                            DateTime loan_date = reader.GetDateTime("loan_date");
                            DateTime? return_date = reader.IsDBNull(reader.GetOrdinal("return_date")) ? (DateTime?)null : reader.GetDateTime("return_date");
                            LoanStatus status = (LoanStatus)Enum.Parse(typeof(LoanStatus), reader.GetString("status"));

                            result = new Loan(id, user_id, book_id, loan_date, return_date, status);
                        }
                        else Console.WriteLine($"Loan with id {id} does not exist");
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
            return "Loans Table";
        }
    }
}
