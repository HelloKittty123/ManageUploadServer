using ManageServer.Entities;
using ManageServer.IServices;
using ManageServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManageServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAccountService _accountService;

        public AccountController(IConfiguration config, IAccountService accountService)
        {
            _config = config;
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var account = await _accountService.ValidateUserAsync(loginModel);
                var token = await GenerateToken(account);

                //// Create cookie options
                //var cookieOptions = new CookieOptions
                //{
                //    Expires = DateTime.Now.AddDays(1), // Set expiration date
                //    HttpOnly = true, // Set to true to prevent client-side access
                //    Secure = true, // Set to true if using HTTPS
                //    SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict // Adjust as necessary for your requirements
                //};

                //Response.Cookies.Append("access_token", token);

                return Ok(new
                {
                    AccessToken = token,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountModel accountModel)
        {
            try
            {
                var account = await _accountService.RegisterUserAsync(accountModel);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("get-account")]
        public async Task<IActionResult> GetAccountAsync()
        {
            try
            {
                var user = HttpContext.User;
                if (user.Identity.IsAuthenticated)
                {
                    var userId = new Guid(user.FindFirst("Id").Value);
                    var account = await _accountService.GetAccountByIdAsync(userId);

                    return Ok(account);

                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAccountAsync([FromBody] AccountModel accountModel)
        {
            try
            {
                var account = await _accountService.CreateUserAsync(accountModel);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateAccountAsync([FromBody] AccountModel accountModel)
        {
            try
            {
                var account = await _accountService.UpdateUserAsync(accountModel);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] UpdatePasswordModel updatePasswordModel)
        {
            try
            {
                //Request.Cookies.TryGetValue("access_token", out string access_token);
                //await _accountService.UpdatePasswordAsync(updatePasswordModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountAsync(Guid id)
        {
            try
            {
                await _accountService.DeleteUserAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<string> GenerateToken(Account account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppSettings:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("Id", account.Id.ToString()),
                new Claim("Email", account.Email),
                new Claim("FirstName", account.FirstName),
                new Claim("LastName", account.LastName),
                new Claim("ExpiredTime", "120")
            };

            var token = new JwtSecurityToken(
                _config["AppSettings:Issuer"],
                _config["AppSettings:Issuer"],
                 claims: claims,
                 expires: DateTime.Now.AddMinutes(120),
                 signingCredentials: credentials
            );

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(                                                   // add new cookie
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties {                                              // remember me
                    IsPersistent = true, 
                    ExpiresUtc = DateTime.Now.AddMinutes(120)
                }                         
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}