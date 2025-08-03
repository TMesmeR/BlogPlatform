using Blog.Core.Entities;
using Blog.Core.Interfaces.Repositories;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogDbContext context) : base(context) { }

        public async Task<IEnumerable<Comment>> GetByPostWithAuthorAsync(int postId)
        {
            return await _context.Comments
                .Include(c => c.Author)
                .Where(c => c.PostId == postId)
                .ToListAsync();
        }
    }
}