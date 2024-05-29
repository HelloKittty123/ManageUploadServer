using ManageServer.IServices;
using ManageServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("getByPage")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProjectPagingAsync(int? page, int? size, string? search = "", Guid? tagId = null)
        {
            try
            {
                var data = await _projectService.GetListProjectPagingAsync(page, size, search, tagId);
                var pagingData = new
                {
                    Data = data,
                    Size = size != null  ? size : data.TotalPage,
                    Page = data.PageIndex,
                    TotalPage = data.TotalPage
                };

                Response.Headers.Add("X-Total-Count", data.Count().ToString());
                return Ok(pagingData);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectByIdAsync(Guid id)
        {
            try
            {
                return Ok(await _projectService.GetProjectByIdAsync(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProjectAsync([FromBody] ProjectModel project)
        {
            try
            {
                var data = await _projectService.AddProjectAsync(project);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdaetProjectAsync([FromBody] ProjectModel project)
        {
            try
            {
                var data = await _projectService.UpdateProjectAsync(project);
                return Ok(data);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectById(Guid id)
        {
            try
            {
                await _projectService.DeleteProjectByIdAsync(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
