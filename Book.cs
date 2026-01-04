using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Projekt
{
    public class Book
    {
        private int id;
        private string title;
        private int published_year;
        private bool isAvailable;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public int Published_year { get => published_year; set => published_year = value; }
        public bool IsAvailable { get => isAvailable; set => isAvailable = value; }

        public Book(int id, string title, int published_year, bool isAvailable)
        {
            Id = id;
            Title = title;
            Published_year = published_year;
            IsAvailable = isAvailable;
        }

        public Book(string title, int published_year, bool isAvailable)
        {
            Id = 0;
            Title = title;
            Published_year = published_year;
            IsAvailable = isAvailable;
        }

        public override string ToString()
        {
            return $"{Id}. {Title} {IsAvailable}";
        }
    }
}
