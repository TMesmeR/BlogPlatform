using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Dtos
{
    public class PostDto
    {
        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [MaxLength(200)]
        public string ShortDescription { get; set; }

        public List<int> TagIds { get; set; } = new();
    }
}