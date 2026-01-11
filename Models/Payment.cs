using System;

namespace WindowsFormsApp1.Models
{
    public class Payment
    {
        private int id;
        private int loan_id;
        private float amount;
        private DateTime payment_date;

        public int Id { get => id; set => id = value; }
        public int Loan_id { get => loan_id; set => loan_id = value; }
        public float Amount { get => amount; set => amount = value; }
        public DateTime Payment_date { get => payment_date; set => payment_date = value; }

        public Payment(int id, int loan_id, float amount, DateTime payment_date)
        {
            Id = id;
            Loan_id = loan_id;
            Amount = amount;
            Payment_date = payment_date;
        }

        public Payment(int loan_id, float amount, DateTime payment_date)
        {
            Loan_id = loan_id;
            Amount = amount;
            Payment_date = payment_date;
        }

        public override string ToString()
        {
            return $"id: {id}, loan_id: {loan_id}, amount: {amount}, payment_date: {payment_date}";
        }
    }
}
