namespace Lab02.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Lead { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }

        // 1 *
        public virtual ICollection<Comment>? Comments { get; set; }
        // * 1
        public virtual Category? Category { get; set; }
        public int? CategoryId { get; set; }
        // * *
        public virtual ICollection<Tag>? Tags { get; set; }
        // * 1
        public virtual Author? Author { get; set; } = null!;
        public int? AuthorId {  get; set; }
        // * 0..1 
        public virtual Match? Match { get; set; } = null!;
        public int? MatchId { get; set;}
    }
}
