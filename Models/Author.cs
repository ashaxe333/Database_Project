namespace WindowsFormsApp1.Models
{
    public class Author
    {
        private int id;
        private string name;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

        public Author(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Author(string name)
        {
            Id = 0;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id}, {Name}";
        }
    }
}
