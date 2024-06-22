using Application.Dtos.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniProjectHub_BE.Services;

namespace UniProjectHub_BE.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICurrentUserService _currentUserService;

        public AdminController(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _currentUserService = currentUserService;
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
            var currentUser = await _currentUserService.GetUser();
            var user = await _userManager.FindByIdAsync(updateUserRoleDto.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Check if the current user is trying to update their own roles
            if (currentUser.Id == user.Id)
            {
                return BadRequest("You cannot update your own roles.");
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            // Check if the current user is an admin and the target user is also an admin
            if (currentRoles.Contains("Admin"))
            {
                return BadRequest("An admin cannot update the roles of another admin.");
            }

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
            var currentUser = await _currentUserService.GetUser();
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (currentUser.Id == user.Id)
            {
                return BadRequest("You cannot deactivate yourself.");
            }

            var currentUserRoles = await _userManager.GetRolesAsync(currentUser);
            var userRoles = await _userManager.GetRolesAsync(user);

            if (currentUserRoles.Contains("Admin") && userRoles.Contains("Admin") && currentUser.Id != user.Id)
            {
                return BadRequest("An admin cannot deactivate another admin.");
            }

            //user.IsActive = isActive;
            //var result = await _userManager.UpdateAsync(user);

            //if (result.Succeeded)
            //{
            //    return Ok(user);
            //}

            return BadRequest("Failed to update user status.");
        }
    }
}
