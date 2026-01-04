using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Genre
    {
        private int id;
        private string name;

        public Genre(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Genre(string name)
        {
            this.Id = 0;
            this.Name = name;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

        public override string ToString()
        {
            return $"{Id}. {Name}";
        }
    }

}
