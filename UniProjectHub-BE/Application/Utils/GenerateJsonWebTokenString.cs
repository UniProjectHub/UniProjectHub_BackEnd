using Domain.Models.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Utils
{
    public static class GenerateJsonWebTokenString
    {
        public static string GenerateJsonWebToken(this User user, string key, DateTime now, IConfiguration configuration )
        {
            var role = user.Role;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, role.Name),
                new Claim("SyllabusPermission", role.SyllabusPermission != null ? role.SyllabusPermission.ToString()! : "AccessDenied"),
                new Claim("TrainingProgramPermission", role.TrainingProgramPermission != null ? role.TrainingProgramPermission.ToString()! : "AccessDenied"),
                new Claim("ClassPermission", role.ClassPermission != null ? role.ClassPermission.ToString()! : "AccessDenied"),
                new Claim("LearningMaterialPermission", role.LearningMaterialPermission != null ? role.LearningMaterialPermission.ToString()! : "AccessDenied"),
                new Claim("UserPermission", role.UserPermission != null ? role.UserPermission.ToString()! : "AccessDenied"),
            };
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims: claims,
                expires: now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static string GenerateJsonWebTokenCustomExpireMinute(this User user, string key, DateTime startTime, int minutes, IConfiguration configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("CreatedDate", startTime.ToString()),
            };
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims: claims,
                expires: startTime.AddMinutes(minutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}