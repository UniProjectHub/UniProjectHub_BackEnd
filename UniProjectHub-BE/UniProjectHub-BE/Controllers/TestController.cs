using Domain.Models;
using Infracstructures.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniProjectHub_BE.Services;

namespace UniProjectHub_BE.Controllers
{
    //[Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class TestController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICurrentUserService _currentUserService;
        public TestController(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _currentUserService = currentUserService;
        }

        // Get all users
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var users = _userManager.Users.Select(user => new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.FirstName,
                user.LastName,
                user.PhoneNumber,
                user.DateOfBirth,
                user.IsStudent,
                user.University,
                user.IsMale,
                user.AvatarURL
            }).ToList();

            return Ok(users);
        }
    }
}
