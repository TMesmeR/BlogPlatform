using System.Collections.Generic;
using System.Xml.Linq;

namespace Blog.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Навигационные свойства
    public List<Post> Posts { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
    public List<UserRole> UserRoles { get; set; } = new();

    // Метод для проверки роли
    public bool IsInRole(string roleName)
        => UserRoles?.Any(ur => ur.Role?.Name == roleName) ?? false;
}