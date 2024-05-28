using BE.Helper;
using ManageServer.Entities;
using ManageServer.Models;

namespace ManageServer.IServices
{
    public interface IProjectService
    {
        Task<Project> AddProjectAsync(ProjectModel project);

        Task<Project> UpdateProjectAsync(ProjectModel project);

        Task DeleteProjectByIdAsync(Guid id);

        Task<Project> GetProjectByIdAsync(Guid id);

        Task<PaginatedList<Project>> GetListProjectPagingAsync(int? page, int? size, string? search, Guid? tagId);
    }
}
