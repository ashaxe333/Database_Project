using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class ActiveLoan
    {
        public int Loan_id { get; set; }
        public string User_name { get; set; }
        public string Book_title { get; set; }
        public DateTime Loan_date { get; set; }
        public LoanStatus Status { get; set; }

        public override string ToString()
        {
            return $"{Loan_id}. {User_name} borrowed '{Book_title}' on {Loan_date} - {Status}";
        }
    }
}