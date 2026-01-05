using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class PaymentSummaryDAO
    {
        public List<PaymentSummary> GetAll()
        {
            List<PaymentSummary> result = new List<PaymentSummary>();
            try
            {
                MySqlConnection conn = DatabaseSingleton.GetInstance();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM view_payment_summary", conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new PaymentSummary
                        {
                            User_id = reader.GetInt32("user_id"),
                            Name = reader.GetString("name"),
                            Payment_count = reader.GetInt32("payment_count"),
                            Total_paid = reader.GetFloat("total_paid"),
                            Min_payment = reader.GetFloat("min_payment"),
                            Max_payment = reader.GetFloat("max_payment")
                        });
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
