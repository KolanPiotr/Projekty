namespace Lab02.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Stadium { get; set; } = string.Empty;
        // 0..1 *
        public virtual ICollection<Article>? Articles { get; set; }
        // 1 *
        public virtual ICollection<MatchEvent>? MatchEvents { get; set; }
        // 1 *
        public virtual ICollection<MatchPlayer>? MatchPlayers { get; set; }
        // * 2
        public virtual Team? HomeTeam { get; set; }
        public int HomeTeamId { get; set; }
        public virtual Team? AwayTeam { get; set; }
        public int AwayTeamId { get; set; }
    }
}
