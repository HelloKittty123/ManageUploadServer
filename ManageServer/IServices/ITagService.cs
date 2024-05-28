using ManageServer.Entities;
using ManageServer.Models;

namespace ManageServer.IServices
{
    public interface ITagService
    {
        Task<List<Tag>> GetAllTagAsync();
        
        Task<Tag> AddTagAsync(TagModel tagModel);

        Task<Tag> UpdateTagAsync(TagModel tagModel);

        Task DeleteTagAsync(Guid id);

        Task<Tag> GetTagByIdAsync(Guid id);
    }
}
