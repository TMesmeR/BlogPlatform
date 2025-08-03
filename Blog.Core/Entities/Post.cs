using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace Blog.Core.Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string? ShortDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Автор статьи
    public int AuthorId { get; set; }
    public User Author { get; set; }

    // Теги статьи
    public List<PostTag> PostTags { get; set; } = new();

    // Комментарии
    public List<Comment> Comments { get; set; } = new();
}