using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Dtos;

public class UserUpdateDto
{
    [MaxLength(50)]
    public string Username { get; set; }

    [EmailAddress]
    public string Email { get; set; }
}