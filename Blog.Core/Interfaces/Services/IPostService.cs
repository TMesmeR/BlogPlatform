using Blog.Core.Dtos;
using Blog.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces.Services
{
    public interface IPostService
    {
        Task<Post> CreatePostAsync(Post post, List<int> tagIds);
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task<Post> UpdatePostAsync(int id, PostDto postDto);
        Task<bool> DeletePostAsync(int id);
        Task<IEnumerable<Post>> GetPostsByTagAsync(int tagId);

    }
}