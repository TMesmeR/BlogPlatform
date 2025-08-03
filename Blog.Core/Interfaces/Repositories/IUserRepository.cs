using Blog.Core.Entities;

namespace Blog.Core.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByEmailAsync(string email);
    Task<User> GetWithRolesAsync(int id);
    Task<bool> UserExistsAsync(string email);
}