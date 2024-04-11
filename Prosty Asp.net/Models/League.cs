namespace Lab02.Models
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int Level { get; set; }
        // 1 *
        public virtual ICollection<Team>? Team { get; set; }
    }
}
