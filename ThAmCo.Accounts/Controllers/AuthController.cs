using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThAmCo.Accounts.Data.Account;
using ThAmCo.Accounts.Models;
using ThAmCo.Accounts.Models.Auth;

namespace ThAmCo.Accounts.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "thamco_api")]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUser([FromBody] UserRegisterDto newUser)
        {
            if (newUser == null)
                return BadRequest();

            var user = new AppUser
            {
                UserName = newUser.Username,
                Email = newUser.Email,
                Forename = newUser.Forename,
                Surname = newUser.Surname
            };

            var result = await _userManager.CreateAsync(user, newUser.Password);
            if (!result.Succeeded)
                return BadRequest();

            user = await _userManager.FindByEmailAsync(newUser.Email);
            await _userManager.AddToRoleAsync(user, "Customer"); //Add user to the "Customer" role by default as it has the lowest permissions.

            var roles = await _userManager.GetRolesAsync(user);

            var dto = new UserResponseDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Fullname = user.Fullname,
                Roles = roles
            };

            return Ok(dto);
        }

        [HttpGet("claims")]
        public async Task<IActionResult> GetUserClaims()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                return BadRequest();

            var claims = identity.Claims.Select(x => new
            {
                x.Type,
                x.Value
            });

            return Ok(claims);
        }
    }
}
