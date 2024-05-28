using ManageServer.Entities;
using ManageServer.IServices;
using ManageServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageServer.Services
{
    public class TagService : ITagService
    {
        private readonly ManageContext _manageContext;

        public TagService(ManageContext manageContext)
        {
            _manageContext = manageContext;
        }

        public async Task<Tag> AddTagAsync(TagModel tagModel)
        {
            var tag = new Tag
            {
                Name = tagModel.Name,
                Description = tagModel.Description,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            await _manageContext.AddAsync(tag);
            await _manageContext.SaveChangesAsync();

            return tag;

        }

        public async Task DeleteTagAsync(Guid id)
        {
            var data = await _manageContext.Tags.FindAsync(id);
            if(data != null) { 
                _manageContext.Tags.Remove(data);
                await _manageContext.SaveChangesAsync();    
            }
        }

        public async Task<List<Tag>> GetAllTagAsync()
        {
            var tagList = await _manageContext.Tags.ToListAsync();
            return tagList;
        }

        public async Task<Tag> GetTagByIdAsync(Guid id)
        {
            var data = await _manageContext.Tags.FindAsync(id);
            return data;
        }

        public async Task<Tag> UpdateTagAsync(TagModel tagModel)
        {
            var data = await _manageContext.Tags.FindAsync(tagModel.Id);
            if(data != null)
            {
                data.Name = tagModel.Name;
                data.Description = tagModel.Description;
                data.UpdatedDate = DateTime.Now;
                await _manageContext.SaveChangesAsync();
            }

            return data;
        }
    }
}
