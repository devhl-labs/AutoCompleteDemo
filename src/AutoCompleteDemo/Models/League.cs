namespace AutoCompleteDemo.Models
{
    public class League
    {
        public int Id { get; }

        public string Name { get; }

        public League(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
