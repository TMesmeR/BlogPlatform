using Blog.Core.Dtos;
using Blog.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Blog.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _tagService.GetAllTagsAsync();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var tag = await _tagService.GetByIdAsync(id);
            if (tag == null)
                return NotFound();

            return Ok(tag);
        }

        [Authorize(Roles = Role.AdminRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TagDto tagDto)
        {
            try
            {
                var tag = await _tagService.CreateTagAsync(tagDto);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = tag.Id },
                    tag);
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
            var result = await _tagService.DeleteTagAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}