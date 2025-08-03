using Blog.Core.Dtos;
using Blog.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces.Services
{
    public interface ITagService
    {
        Task<Tag> CreateTagAsync(TagDto tagDto);
        Task<IEnumerable<Tag>> GetAllTagsAsync();
        Task<Tag> GetByIdAsync(int id);
        Task<bool> DeleteTagAsync(int id);
        Task<bool> TagExistsAsync(string name);
        Task<bool> TagExistsAsync(int id);
    }
}