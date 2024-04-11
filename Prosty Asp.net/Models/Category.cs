namespace Lab02.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // 1 * 
        public virtual ICollection<Article>? Articles { get; set; }
    }
}