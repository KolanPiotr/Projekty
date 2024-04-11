namespace Lab02.Models
{
    public class MatchEvent
    {
        public int Id { get; set; }
        public int Minute { get; set; }

        // * 1
        public virtual EventType? EventType { get; set; }
        public int EventTypeId { get; set; }
        // * 0..1
        public virtual MatchPlayer? MatchPlayer { get; set; }
        public int MatchPlayerId { get; set; }
        // * 1
        public virtual Match? Match { get; set; }
        public int MatchId { get; set; }
    }
}
