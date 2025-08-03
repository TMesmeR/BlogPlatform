using Blog.Core.Entities;
using Blog.Core.Interfaces.Repositories;
using Blog.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Dtos;

namespace Blog.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagService _tagService;
        private readonly IUserRepository _userRepository;

        public PostService(
            IPostRepository postRepository,
            ITagService tagService,
            IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _tagService = tagService;
            _userRepository = userRepository;
        }

        public async Task<Post> CreatePostAsync(Post post, List<int> tagIds)
        {
            if (await _userRepository.GetByIdAsync(post.AuthorId) == null)
                throw new KeyNotFoundException("Author not found");

            post.PostTags = new List<PostTag>();
            foreach (var tagId in tagIds.Distinct())
            {
                if (await _tagService.GetByIdAsync(tagId) is Tag tag)
                {
                    post.PostTags.Add(new PostTag { TagId = tag.Id });
                }
            }

            post.CreatedAt = DateTime.UtcNow;
            await _postRepository.AddAsync(post);
            return post;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _postRepository.GetPostsWithTagsAsync();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _postRepository.GetPostWithCommentsAsync(id);
        }

        public async Task<Post> UpdatePostAsync(int id, PostDto postDto)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
                return null;

            post.Title = postDto.Title;
            post.Content = postDto.Content;
            post.ShortDescription = postDto.ShortDescription;
            post.UpdatedAt = DateTime.UtcNow;

            // Update tags
            if (postDto.TagIds != null)
            {
                post.PostTags ??= new List<PostTag>();
                var currentTagIds = post.PostTags.Select(pt => pt.TagId).ToList();
                var newTagIds = postDto.TagIds.Distinct().ToList();

                // Remove tags
                post.PostTags.RemoveAll(pt => !newTagIds.Contains(pt.TagId));

                // Add new tags
                foreach (var tagId in newTagIds.Except(currentTagIds))
                {
                    if (await _tagService.TagExistsAsync(tagId))
                    {
                        post.PostTags.Add(new PostTag { TagId = tagId });
                    }
                }
            }

            await _postRepository.UpdateAsync(post);
            return post;
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
                return false;

            await _postRepository.DeleteAsync(post);
            return true;
        }

        public async Task<IEnumerable<Post>> GetPostsByTagAsync(int tagId)
        {
            return await _postRepository.FindAsync(p =>
                p.PostTags.Any(pt => pt.TagId == tagId));
        }
    }
}