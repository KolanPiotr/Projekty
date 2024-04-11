namespace Lab02.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime FoundingDate { get; set; }
        // 1 *
        public virtual ICollection<Player>? Players { get; set; }
        // * 1
        public virtual League? League { get; set; }
        public int LeagueId { get; set; }
        // 2 *
        public virtual ICollection<Match>? HomeMatches { get; set; }
        public virtual ICollection<Match>? AwayMatches { get; set; }
    }
}
