using BE.Helper;
using ManageServer.Entities;
using ManageServer.IServices;
using ManageServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ManageServer.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ManageContext _manageContext;
        private const int SIZE = 10;

        public ProjectService(ManageContext manageContext)
        {
            _manageContext = manageContext;
        }

        public async Task<Project> AddProjectAsync(ProjectModel project)
        {
            var projectNew = new Project
            {
                Name = project.Name,
                DataProject = project.DataProject,
                Description = project.Description,
                TagId = project.TagId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            await _manageContext.AddAsync(projectNew);
            await _manageContext.SaveChangesAsync();
            return projectNew;
        }

        public async Task DeleteProjectByIdAsync(Guid id)
        {
            var data = await _manageContext.Projects.FindAsync(id);
            if (data != null)
            {
                _manageContext.Remove(data);
                await _manageContext.SaveChangesAsync();
            }
        }

        public async Task<PaginatedList<Project>> GetListProjectPagingAsync(int? page, int? size, string? search, Guid? tagId)
        {
            var dataSet = _manageContext.Projects;
            IQueryable<Project> data;
            if(tagId != null)
            {
                data = dataSet.Where(d => d.TagId.Equals(tagId)).OrderByDescending(p => p.CreatedDate).Where(p => p.Name.Contains(search));
            }
            else
            {
                data = dataSet.OrderByDescending(p => p.CreatedDate).Where(p => p.Name.Contains(search));
            }
            var paginator = PaginatedList<Project>.Create(data, page, size);

            return paginator;
        }

        public async Task<Project> GetProjectByIdAsync(Guid id)
        {
            var data = await _manageContext.Projects.FindAsync(id);
            return data;
        }

        public async Task<Project> UpdateProjectAsync(ProjectModel project)
        {
            var data = _manageContext.Projects.Find(project.Id);
            if (data != null)
            {
                data.Name = project.Name;
                data.Description = project.Description;
                data.DataProject = project.DataProject;
                data.TagId = project.TagId;
                data.UpdatedDate = DateTime.Now;
            }

            return data;
        }
    }
}
