namespace Lab02.Models
{
    public class MatchPlayer
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // 0..1 *
        public virtual ICollection<MatchEvent>? MatchEvents { get; set; } = null!;
        // * 1
        public virtual Match? Match { get; set; }
        public int? MatchId { get; set; }
        // * 1
        public virtual Position? Position { get; set; }
        public int? PositionId { get; set; }
        // * 1
        public virtual Player? Player { get; set; }
        public int? PlayerId { get; set; }
        
    }
}
