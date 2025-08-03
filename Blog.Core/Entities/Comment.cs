namespace Blog.Core.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Автор комментария
    public int AuthorId { get; set; }
    public User Author { get; set; }

    // Статья, к которой относится комментарий
    public int PostId { get; set; }
    public Post Post { get; set; }
}