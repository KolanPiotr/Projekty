using Lab02.Models;

public class Comment
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content {  get; set; } = string.Empty;
    // * 1
    public virtual Article? Article { get; set; } = null!;
    public int? ArticleId { get; set; }
}