using MySql.Data.MySqlClient;
using WindowsFormsApp1.Database;
using WindowsFormsApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            try
            {
                MySqlConnection conn = DatabaseSingleton.GetInstance();

                Console.WriteLine("view 'view_active_loans':");
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM view_active_loans", conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            /*
                            LoanStatus status = LoanStatus.RETURNED;

                            int statusIndex = reader.GetOrdinal("status");
                            if (!reader.IsDBNull(statusIndex))
                            {
                                string statusStr = reader.GetString(statusIndex);
                                Enum.TryParse(statusStr, out status);
                            }
                            */

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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
