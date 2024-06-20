using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(Users user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        void SaveRefreshToken(string userId, string refreshToken);
        void RemoveRefreshToken(string refreshToken);
        RefreshToken GetRefreshToken(string refreshToken);
        string GenerateDownloadFileToken(string fileName);
        bool ValidateToken(string token, out string fileName);
    }
}