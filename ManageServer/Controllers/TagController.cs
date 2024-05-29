using ManageServer.IServices;
using ManageServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllTagAsync()
        {
            try
            {
                var data = await _tagService.GetAllTagAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagByIdAsync(Guid id)
        {
            try
            {
                var data = await _tagService.GetTagByIdAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
 
        [HttpPost]
        public async Task<IActionResult> AddTagAsync(TagModel tagModel)
        {
            try
            {
                var data = await _tagService.AddTagAsync(tagModel);
                return Ok(data);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTagAsync(TagModel tagModel)
        {
            try
            {
                var data = await _tagService.UpdateTagAsync(tagModel);
                return Ok(data);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]        
        public async Task<IActionResult> DeleteTagByIdAsync(Guid id)
        {
            try
            {
                await _tagService.DeleteTagAsync(id);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}
