using Blog.Core.Entities;

namespace Blog.Core.Interfaces.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role> GetByNameAsync(string name);
}