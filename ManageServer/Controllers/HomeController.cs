using ManageServer.IServices;
using ManageServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController :  ControllerBase
    {
        private readonly IHomeService _homeService;
        
        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _homeService.GetAllHomeAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHomeById(Guid id)
        {
            try
            {
                return Ok(await _homeService.GetHomeById(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddHomeAsync([FromBody] HomeModel homeModel)
        {
            try
            {
                var data = await _homeService.AddHomeAsync(homeModel);
                return Ok(data);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHomeAsync([FromBody] HomeModel homeModel)
        {
            try
            {
                var data = await _homeService.UpdateHomeAsync(homeModel);
                return Ok(data);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHomeItem(Guid id)
        {
            try
            {
                await _homeService.DeleteHomeAsync(id);
                return Ok();
            }
            catch(Exception e) 
            { 
                return BadRequest(e.Message);
            }
        }
    }
}
