using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace UniProjectHub_BE.Services
{
    public interface ICurrentUserService
    {
        Guid GetUserId();
        String getUserEmail();
        Task<Users> User();
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

        public Guid GetCurrentUserId()
        {
            throw new NotImplementedException();
        }
        public Guid GetUserId()
        {
            return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        }
        public String getUserEmail()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        }

        public async Task<Users> User()
        {
            var userId = GetUserId();
            return await _userManager.FindByIdAsync(userId.ToString());
        }
    }
}
