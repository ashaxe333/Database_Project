using MySql.Data.MySqlClient;
using WindowsFormsApp1.Database;
using WindowsFormsApp1.Models;
using System;
using System.Collections.Generic;

namespace WindowsFormsApp1.DAO
{
    public class ActiveLoanDAO
    {
        /// <summary>
        /// Selects all from view 'view_active_loans'
        /// </summary>
        /// <returns> List of ActiveLoans </returns>
        public List<ActiveLoan> GetAll()
        {
            List<ActiveLoan> result = new List<ActiveLoan>();
            MySqlConnection conn = DatabaseSingleton.GetInstance();

            Console.WriteLine("view 'view_active_loans':");
            using (MySqlCommand command = new MySqlCommand("SELECT * FROM view_active_loans", conn))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new ActiveLoan
                        {
                            Loan_id = reader.GetInt32("loan_id"),
                            User_name = reader.GetString("user_name"),
                            Book_title = reader.GetString("book_title"),
                            Loan_date = reader.GetDateTime("loan_date"),
                            Status = (LoanStatus)Enum.Parse(typeof(LoanStatus), reader.GetString("status"))
                        });
                    }
                }
            }

            return result;
        }
    }
}
