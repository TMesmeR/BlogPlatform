using Blog.Core.Dtos;
using Blog.Core.Entities;

namespace Blog.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(User user, string password);
        Task<string> AuthenticateAsync(string email, string password);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetByIdAsync(int id);
        Task UpdateUserAsync(int id, UserUpdateDto userDto);
        Task DeleteAsync(int id);
        Task AssignRoleAsync(int userId, int roleId);
        Task<bool> UserExistsAsync(string email);
    }
}