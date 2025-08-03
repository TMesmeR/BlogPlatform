using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Dtos;

public class UserRegisterDto
{
    [Required, MaxLength(50)]
    public string Username { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, MinLength(8)]
    public string Password { get; set; }
}