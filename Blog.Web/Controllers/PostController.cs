using System.Security.Claims;
using Blog.Core.Dtos;
using Blog.Core.Entities;
using Blog.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostDto postDto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var post = new Post
                {
                    Title = postDto.Title,
                    Content = postDto.Content,
                    ShortDescription = postDto.ShortDescription,
                    AuthorId = userId
                };

                var createdPost = await _postService.CreatePostAsync(post, postDto.TagIds);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdPost.Id },
                    createdPost);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [Authorize(Roles = $"{Role.AdminRoleName},{Role.ModeratorRoleName}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PostDto postDto)
        {
            try
            {
                var updatedPost = await _postService.UpdatePostAsync(id, postDto);
                if (updatedPost == null)
                    return NotFound();

                return Ok(updatedPost);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = Role.AdminRoleName)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _postService.DeletePostAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("by-tag/{tagId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByTag(int tagId)
        {
            var posts = await _postService.GetPostsByTagAsync(tagId);
            return Ok(posts);
        }
    }
}