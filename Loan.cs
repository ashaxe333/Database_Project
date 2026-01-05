namespace Projekt
{
    public class Loan
    {
        private int id;
        private int user_id;
        private int book_id;
        private DateTime loan_date;
        private DateTime? return_date;
        private LoanStatus status;

        public int Id { get => id; set => id = value; }
        public int User_id { get => user_id; set => user_id = value; }
        public int Book_id { get => book_id; set => book_id = value; }
        public DateTime Loan_date { get => loan_date; set => loan_date = value; }
        public DateTime? Return_date { get => return_date; set => return_date = value; }
        public LoanStatus Status { get => status; set => status = value; }

        public Loan(int id, int user_id, int book_id, DateTime loan_date, DateTime? return_date, LoanStatus status)
        {
            Id = id;
            User_id = user_id;
            Book_id = book_id;
            Loan_date = loan_date;
            Return_date = return_date;
            Status = status;
        }

        public Loan(int user_id, int book_id, DateTime loan_date, DateTime? return_date, LoanStatus status)
        {
            Id = 0;
            User_id = user_id;
            Book_id = book_id;
            Loan_date = loan_date;
            Return_date = return_date;
            Status = status;
        }

        public override string ToString()
        {
            return $"id: {Id}, user_id: {User_id}, book_id: {Book_id}, loan_date: {Loan_date}, retur_date: {Return_date}, status: {Status}";
        }
    }
}
