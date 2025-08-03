using Blog.Core.Dtos;
using Blog.Core.Entities;
using Blog.Core.Interfaces.Repositories;
using Blog.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly IPostRepository _postRepository;
        private readonly IRepository<Comment> _commentRepository;

        public CommentService(
            IPostRepository postRepository,
            IRepository<Comment> commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        public async Task<Comment> CreateAsync(CommentDto commentDto, int authorId)
        {
            if (await _postRepository.GetByIdAsync(commentDto.PostId) == null)
                throw new KeyNotFoundException("Post not found");

            var comment = new Comment
            {
                Text = commentDto.Text,
                PostId = commentDto.PostId,
                AuthorId = authorId,
                CreatedAt = DateTime.UtcNow
            };

            await _commentRepository.AddAsync(comment);
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetAllByPostAsync(int postId)
        {
            return await _commentRepository.FindAsync(c => c.PostId == postId);
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _commentRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, CommentDto commentDto, int userId)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null || comment.AuthorId != userId)
                return false;

            comment.Text = commentDto.Text;
            await _commentRepository.UpdateAsync(comment);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null || comment.AuthorId != userId)
                return false;

            await _commentRepository.DeleteAsync(comment);
            return true;
        }
    }
}