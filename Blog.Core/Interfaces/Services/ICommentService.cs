using Blog.Core.Dtos;
using Blog.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces.Services
{
    public interface ICommentService
    {
        Task<Comment> CreateAsync(CommentDto commentDto, int authorId);
        Task<IEnumerable<Comment>> GetAllByPostAsync(int postId);
        Task<Comment> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, CommentDto commentDto, int userId);
        Task<bool> DeleteAsync(int id, int userId);
    }
}