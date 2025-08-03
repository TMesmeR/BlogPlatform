using Blog.Core.Entities;

namespace Blog.Core.Interfaces.Repositories;

public interface IPostRepository : IRepository<Post>
{
    Task<IEnumerable<Post>> GetPostsWithTagsAsync();
    Task<Post> GetPostWithCommentsAsync(int id);
    Task<IEnumerable<Post>> GetPostsByAuthorAsync(int authorId);
}