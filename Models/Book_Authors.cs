using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class Book_Authors
    {
        private int id;
        private int book_id;
        private int author_id;

        public Book_Authors(int id, int book_id, int author_id)
        {
            Id = id;
            Book_id = book_id;
            Author_id = author_id;
        }

        public Book_Authors(int book_id, int author_id)
        {
            Id = 0;
            Book_id = book_id;
            Author_id = author_id;
        }

        public int Id { get => id; set => id = value; }
        public int Book_id { get => book_id; set => book_id = value; }
        public int Author_id { get => author_id; set => author_id = value; }

        public override string ToString()
        {
            return $"id: {Id}, book_id: {Book_id}, author_id: {Author_id}";
        }
    }
}
