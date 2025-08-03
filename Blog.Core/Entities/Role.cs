namespace Blog.Core.Entities;

public class Role
{

    public int Id { get; set; }
    public string Name { get; set; } // "Admin", "Moderator", "User"
    public string Description { get; set; }

    public List<UserRole> UserRoles { get; set; } = new();

    // Константы для стандартных ролей
    public const string AdminRoleName = "Admin";
    public const string UserRoleName = "User";
    public const string ModeratorRoleName = "Moderator";
}