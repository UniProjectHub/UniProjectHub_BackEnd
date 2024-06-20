using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Domain.Data;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace Infracstructures.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<Users> _userManager;
        private static List<RefreshToken> _refreshTokens = new List<RefreshToken>();
        private readonly IMemoryCache _cache;
        private readonly ConcurrentDictionary<string, string> _tokenStore = new ConcurrentDictionary<string, string>();

        public TokenService(IConfiguration config, UserManager<Users> userManage, IMemoryCache cache)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
            _userManager = userManage;
            _cache = cache;
        }

        public async Task<string> GenerateToken(Users user)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var userRole = await _userManager.GetRolesAsync(user);
            foreach (var role in userRole)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(double.Parse(_config["JWT:AccessTokenExpiration"])), // Use minutes for access token expiration
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false, // Ignore token expiration
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["JWT:Issuer"],
                ValidAudience = _config["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public void SaveRefreshToken(string userId, string refreshToken)
        {
            _refreshTokens.Add(new RefreshToken { 
                UserId = userId, 
                Token = refreshToken, 
                ExpiryDate = DateTime.UtcNow.AddDays(double.Parse(_config["JWT:RefreshTokenExpiration"])) 
            });
        }

        public RefreshToken GetRefreshToken(string refreshToken)
        {
            return _refreshTokens.SingleOrDefault(rt => rt.Token == refreshToken);
        }

        public void RemoveRefreshToken(string refreshToken)
        {
            var token = _refreshTokens.SingleOrDefault(rt => rt.Token == refreshToken);
            if (token != null)
            {
                _refreshTokens.Remove(token);
            }
        }

        public void RemoveUserRefreshTokens(string userId)
        {
            _refreshTokens.RemoveAll(t => t.UserId == userId);
        }

        public string GenerateDownloadFileToken(string fileName)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            _tokenStore[token] = fileName;

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1)); // Token expires in 1 hour
            _cache.Set(token, fileName, cacheEntryOptions);

            return token;
        }

        public bool ValidateToken(string token, out string fileName)
        {
            return _cache.TryGetValue(token, out fileName);
        }
    }
}