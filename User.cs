using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Projekt
{
    public class User
    {
        private int id;
        private string name;
        private string email;
        private bool is_active;
        private DateTime created_at;
        public User(int id, string name, string email, bool is_active, DateTime created_at)
        {
            Id = id;
            Name = name;
            Email = email;
            Is_active = is_active;
            Created_at = created_at;
        }

        public User(string name, string email, bool is_active, DateTime created_at)
        {
            Id = 0;
            Name = name;
            Email = email;
            Is_active = is_active;
            Created_at = created_at;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public bool Is_active { get => is_active; set => is_active = value; }
        public DateTime Created_at { get => created_at; set => created_at = value; }

        public override string ToString()
        {
            return $"id: {Id}, name: {Name}, email: {Email}, is_active: {Is_active}, created_at: {Created_at}";
        }
    }
}
