using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Loan
    {
        private int id;
        private int user_id;
        private int book_id;
        private DateTime loanDate;
        private DateTime returnDate;
        private LoanStatus loanStatus;

        public int Id { get => id; set => id = value; }
        public int User_id { get => user_id; set => user_id = value; }
        public int Book_id { get => book_id; set => book_id = value; }
        public DateTime LoanDate { get => loanDate; set => loanDate = value; }
        public DateTime ReturnDate { get => returnDate; set => returnDate = value; }
        public LoanStatus LoanStatus { get => loanStatus; set => loanStatus = value; }

        public Loan(int id, int user_id, int book_id, DateTime loanDate, DateTime returnDate, LoanStatus loanStatus)
        {
            Id = id;
            User_id = user_id;
            Book_id = book_id;
            LoanDate = loanDate;
            ReturnDate = returnDate;
            LoanStatus = loanStatus;
        }

        public Loan(int user_id, int book_id, DateTime borrowDate, DateTime returnDate, LoanStatus loanStatus)
        {
            Id = 0;
            User_id = user_id;
            Book_id = book_id;
            LoanDate = borrowDate;
            ReturnDate = returnDate;
            LoanStatus = loanStatus;
        }

        public override string ToString()
        {
            return $"{Id}. {User_id} {Book_id} {LoanDate}";
        }
    }
}
