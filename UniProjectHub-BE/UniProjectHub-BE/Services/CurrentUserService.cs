using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace UniProjectHub_BE.Services
{
    public interface ICurrentUserService
    {
        string GetUserId();
        string GetUserEmail();
        Task<Users> GetUser();
    }

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Users> _userManager;
        private readonly IActionContextAccessor _actionContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IActionContextAccessor actionContextAccessor,
            UserManager<Users> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _actionContextAccessor = actionContextAccessor;
        }

        public string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId), "User ID not found in claims.");
            }
            return userId;
        }

        public string GetUserEmail()
        {
            var userEmail = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                throw new ArgumentNullException(nameof(userEmail), "User email not found in claims.");
            }
            return userEmail;
        }

        public async Task<Users> GetUser()
        {
            var userId = GetUserId();
            return await _userManager.FindByIdAsync(userId.ToString());
        }
    }
}
