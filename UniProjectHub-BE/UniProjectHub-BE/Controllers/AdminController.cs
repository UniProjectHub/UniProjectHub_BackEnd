using Application.Dtos.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UniProjectHub_BE.Controllers
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        // Get all users
        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            
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

        // Update user roles
        [HttpPost("update-role")]
        public async Task<IActionResult> UpdateUserRole(UpdateUserRoleDto updateUserRoleDto)
        {
            var user = await _userManager.FindByIdAsync(updateUserRoleDto.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!result.Succeeded)
            {
                return BadRequest("Failed to remove user roles.");
            }

            result = await _userManager.AddToRolesAsync(user, updateUserRoleDto.Roles);
            if (!result.Succeeded)
            {
                return BadRequest("Failed to add user roles.");
            }

            return Ok(new { message = "User roles updated successfully." });
        }

        // Delete user
        [HttpDelete("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest("Failed to delete user.");
            }

            return Ok(new { message = "User deleted successfully." });
        }
    }
}
