namespace Lab02.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        // * 1
        public virtual Team? Team { get; set; }
        public int? TeamId { get; set; }
            // * 1..*
        public virtual ICollection<Position>? Positions { get; set; }
        // 1 *
        public virtual ICollection<MatchPlayer>? MatchPlayers { get; set; }
    }
}
