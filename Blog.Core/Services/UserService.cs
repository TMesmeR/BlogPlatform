using Blog.Core.Entities;
using Blog.Core.Interfaces.Repositories;
using Blog.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Dtos;

namespace Blog.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly JwtService _jwtService;

        public UserService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            JwtService jwtService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _jwtService = jwtService;
        }

        public async Task<User> RegisterAsync(User user, string password)
        {
            if (await _userRepository.GetByEmailAsync(user.Email) != null)
                throw new ArgumentException("Email already exists");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
            user.CreatedAt = DateTime.UtcNow;
            user.UserRoles = new List<UserRole>();

            // Assign default User role
            var defaultRole = await _roleRepository.GetByNameAsync(Role.UserRoleName);
            if (defaultRole != null)
            {
                user.UserRoles.Add(new UserRole { RoleId = defaultRole.Id });
            }

            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            return _jwtService.GenerateToken(user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task UpdateUserAsync(int id, UserUpdateDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            if (!string.IsNullOrWhiteSpace(userDto.Username))
                user.Username = userDto.Username;

            if (!string.IsNullOrWhiteSpace(userDto.Email))
            {
                if (await _userRepository.GetByEmailAsync(userDto.Email) != null)
                    throw new ArgumentException("Email already in use");
                user.Email = userDto.Email;
            }

            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            await _userRepository.DeleteAsync(user);
        }

        public async Task AssignRoleAsync(int userId, int roleId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var role = await _roleRepository.GetByIdAsync(roleId);

            if (user == null || role == null)
                throw new KeyNotFoundException("User or role not found");

            if (user.UserRoles.Any(ur => ur.RoleId == roleId))
                return;

            user.UserRoles.Add(new UserRole { RoleId = roleId });
            await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email) != null;
        }
    }
}
