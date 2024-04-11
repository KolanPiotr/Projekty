namespace Lab02.Models
{
    public class EventType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        // 1 *
        public virtual ICollection<MatchEvent>? MatchEvents { get; set; }
    }
}
