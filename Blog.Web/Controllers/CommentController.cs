using System.Security.Claims;
using Blog.Core.Dtos;
using Blog.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommentDto commentDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var comment = await _commentService.CreateAsync(commentDto, userId);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment);
        }

        [HttpGet("by-post/{postId}")]
        public async Task<IActionResult> GetAllByPost(int postId)
        {
            var comments = await _commentService.GetAllByPostAsync(postId);
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
                return NotFound();
            return Ok(comment);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CommentDto commentDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _commentService.UpdateAsync(id, commentDto, userId);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _commentService.DeleteAsync(id, userId);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}