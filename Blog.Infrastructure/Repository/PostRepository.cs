using Blog.Core.Entities;
using Blog.Core.Interfaces.Repositories;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(BlogDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Post>> GetPostsWithTagsAsync()
        {
            return await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tag)
                .ToListAsync();
        }

        public async Task<Post> GetPostWithCommentsAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Author)
                .Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetPostsByAuthorAsync(int authorId)
        {
            return await _context.Posts
                .Where(p => p.AuthorId == authorId)
                .Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tag)
                .ToListAsync();
        }
    }
}