using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThAmCo.Accounts.Data.Account;
using ThAmCo.Accounts.Interfaces;
using ThAmCo.Accounts.Models;
using ThAmCo.Accounts.Models.Auth;
using ThAmCo.Accounts.Models.Profile;

namespace ThAmCo.Accounts.Controllers.API
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "thamco_api")]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IProfileService _profileService;

        public AuthController(UserManager<AppUser> userManager, IProfileService profileService)
        {
            _userManager = userManager;
            _profileService = profileService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromForm] UserLoginDto loginInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return await LoginAndStoreCookie(loginInfo.Username, loginInfo.Password);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUser([FromForm] UserRegisterDto newUser)
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

            await _profileService.AddProfile(new ProfileDto //Create a profile in the profile micro-service that corresponds with this user.
            {
                Id = Guid.Parse(user.Id),
                Username = user.UserName,
                Email = user.Email,
                Forename = user.Forename,
                Surname = user.Surname
            });

            return await LoginAndStoreCookie(newUser.Username, newUser.Password);
        }

        private async Task<IActionResult> LoginAndStoreCookie(string username, string password)
        {
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}";
            var tokenUrl = "/connect/token";

            var client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            var data = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", "thamcoApiClient"),
                new KeyValuePair<string, string>("scope", "thamco_api openid profile roles"),
                new KeyValuePair<string, string>("response_type", "token"),

                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            };

            using var content = new FormUrlEncodedContent(data);
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var response = await client.PostAsync(tokenUrl, content);

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index", "Auth");

            var loginResponse = await response.Content.ReadAsAsync<AuthLoginResponse>();

            var cookieOptions = new CookieOptions { Expires = DateTime.Now.AddSeconds(loginResponse.ExpiresInSeconds), Domain = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}" };
            Response.Cookies.Append("access_token", loginResponse.AccessToken, cookieOptions);

            return RedirectToAction("Index", "Home");
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
