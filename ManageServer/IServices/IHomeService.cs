using ManageServer.Entities;
using ManageServer.Models;

namespace ManageServer.IServices
{
    public interface IHomeService
    {
        Task<List<Home>> GetAllHomeAsync();

        Task<Home> AddHomeAsync(HomeModel home);

        Task<Home> UpdateHomeAsync(HomeModel home);

        Task DeleteHomeAsync(Guid id);

        Task<Home> GetHomeById(Guid id);

    }
}
