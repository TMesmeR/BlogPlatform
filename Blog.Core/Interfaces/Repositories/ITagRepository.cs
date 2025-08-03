using Blog.Core.Entities;

namespace Blog.Core.Interfaces.Repositories;

public interface ITagRepository : IRepository<Tag>
{
    Task<Tag> GetByNameAsync(string name);

}