using Blog.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetByPostWithAuthorAsync(int postId);
    }
}