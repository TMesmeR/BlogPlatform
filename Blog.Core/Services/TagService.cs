using Blog.Core.Dtos;
using Blog.Core.Entities;
using Blog.Core.Interfaces.Repositories;
using Blog.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Tag> CreateTagAsync(TagDto tagDto)
        {
            if (await _tagRepository.GetByNameAsync(tagDto.Name) != null)
                throw new ArgumentException("Tag with this name already exists");

            var tag = new Tag
            {
                Name = tagDto.Name.Trim()
            };

            await _tagRepository.AddAsync(tag);
            return tag;
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await _tagRepository.GetAllAsync();
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            return await _tagRepository.GetByIdAsync(id);
        }

        public async Task<bool> DeleteTagAsync(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag == null)
                return false;

            await _tagRepository.DeleteAsync(tag);
            return true;
        }

        public async Task<bool> TagExistsAsync(string name)
        {
            return await _tagRepository.GetByNameAsync(name) != null;
        }

        public async Task<bool> TagExistsAsync(int id)
        {
            return await _tagRepository.GetByIdAsync(id) != null;
        }
    }
}
