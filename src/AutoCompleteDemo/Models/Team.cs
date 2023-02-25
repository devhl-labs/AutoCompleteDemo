namespace AutoCompleteDemo.Models
{
    public class Team
    {
        public int Id { get; }

        public int LeaugeId { get; }

        public string Name { get; }

        public Team(int id, int leaugeId, string name)
        {
            Id = id;
            LeaugeId = leaugeId;
            Name = name;
        }
    }
}
