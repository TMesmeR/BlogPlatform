using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(BlogDbContext context)
        {
            await context.Database.MigrateAsync();

            if (await context.Users.AnyAsync())
                return;

            // Создаем роли, если их нет
            if (!await context.Roles.AnyAsync())
            {
                var roles = new List<Role>
                {
                    new() { Id = 1, Name = Role.AdminRoleName, Description = "Administrator" },
                    new() { Id = 2, Name = Role.ModeratorRoleName, Description = "Moderator" },
                    new() { Id = 3, Name = Role.UserRoleName, Description = "User" }
                };

                await context.Roles.AddRangeAsync(roles);
                await context.SaveChangesAsync();
            }

            // Создаем тестовых пользователей
            var users = new List<User>
            {
                new()
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    CreatedAt = DateTime.UtcNow,
                    UserRoles = new List<UserRole> { new() { RoleId = 1 } } // Admin
                },
                new()
                {
                    Id = 2,
                    Username = "moderator",
                    Email = "moderator@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Moderator123!"),
                    CreatedAt = DateTime.UtcNow,
                    UserRoles = new List<UserRole> { new() { RoleId = 2 } } // Moderator
                },
                new()
                {
                    Id = 3,
                    Username = "user",
                    Email = "user@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!"),
                    CreatedAt = DateTime.UtcNow,
                    UserRoles = new List<UserRole> { new() { RoleId = 3 } } // User
                }
            };

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    }
}