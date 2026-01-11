using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Database;

namespace WindowsFormsApp1.DAO
{
    public class PaymentDAO : IDAO<Payment>
    {
        /// <summary>
        /// Deletes payment from the table by id
        /// </summary>
        /// <param name="id"> payment id </param>
        public void Delete(int id)
        {
            Console.WriteLine("deleting payments");
            MySqlConnection conn = DatabaseSingleton.GetInstance();

            using (MySqlCommand command = new MySqlCommand("DELETE FROM payments WHERE id = @id", conn))
            {
                command.Parameters.Add(new MySqlParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Inserts or updates pament based on id
        /// </summary>
        /// <param name="payment"> payment to insert or update </param>
        public void Save(Payment payment, MySqlTransaction transaction)
        {

            MySqlConnection conn = DatabaseSingleton.GetInstance();

            MySqlCommand command = null;

            if (payment.Id < 1)
            {
                Console.WriteLine("inserting payments");
                using (command = new MySqlCommand("INSERT INTO payments (loan_id, amount, payment_date) VALUES (@loan_id, @amount, @payment_date)", conn))
                {
                    command.Parameters.Add(new MySqlParameter("@loan_id", payment.Loan_id));
                    command.Parameters.Add(new MySqlParameter("@amount", payment.Amount));
                    command.Parameters.Add(new MySqlParameter("@payment_date", payment.Payment_date));
                    command.ExecuteNonQuery();

                    command.CommandText = "Select LAST_INSERT_ID()";
                    payment.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                Console.WriteLine("updating payments");
                using (command = new MySqlCommand("UPDATE payments SET loan_id = @loan_id, amount = @amount, payment_date = @payment_date WHERE id = @id", conn))
                {
                    command.Parameters.Add(new MySqlParameter("@id", payment.Id));
                    command.Parameters.Add(new MySqlParameter("@loan_id", payment.Loan_id));
                    command.Parameters.Add(new MySqlParameter("@amount", payment.Amount));
                    command.Parameters.Add(new MySqlParameter("@payment_date", payment.Payment_date));
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Gets all payments
        /// </summary>
        /// <returns> List of Payments </returns>
        public List<Payment> GetAll()
        {
            List<Payment> result = new List<Payment>();
            MySqlConnection conn = DatabaseSingleton.GetInstance();

            Console.WriteLine("get all payments");
            using (MySqlCommand command = new MySqlCommand("SELECT id, loan_id, amount, payment_date FROM payments", conn)) //1 connection, 1 reader v jeden moment
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        int loan_id = reader.GetInt32("loan_id");
                        float amount = reader.GetFloat("amount");
                        DateTime payment_date = reader.GetDateTime("payment_date");

                        result.Add(new Payment(id, loan_id, amount, payment_date));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets payment from the table by id
        /// </summary>
        /// <param name="id"> payment id </param>
        /// <returns> Payment object </returns>
        public Payment GetById(int id)
        {
            Payment result = null;
            Console.WriteLine("getting payment");
            MySqlConnection conn = DatabaseSingleton.GetInstance();

            using (MySqlCommand command = new MySqlCommand("SELECT loan_id, amount, payment_date FROM payments WHERE id = @id", conn))
            {
                command.Parameters.Add(new MySqlParameter("@id", id));

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int loan_id = reader.GetInt32("loan_id");
                        float amount = reader.GetFloat("amount");
                        DateTime payment_date = reader.GetDateTime("payment_date");

                        result = new Payment(id, loan_id, amount, payment_date);
                    }
                    else Console.WriteLine($"Payment with id {id} does not exist");
                }
            }

            return result;
        }

        public override string ToString()
        {
            return "Payment Table";
        }
    }
}
