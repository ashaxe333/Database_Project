using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class PaymentSummary
    {
        public int User_id { get; set; }
        public string Name { get; set; }
        public int Payment_count { get; set; }
        public float Total_paid { get; set; }
        public float Min_payment { get; set; }
        public float Max_payment { get; set; }

        public override string ToString()
        {
            return $"{Name} (ID: {User_id}) - Payments: {Payment_count}, Total: {Total_paid}, Min: {Min_payment}, Max: {Max_payment}";
        }
    }

}
