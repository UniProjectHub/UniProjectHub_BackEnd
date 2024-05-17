using System.IdentityModel.Tokens.Jwt;

namespace Application.Utils
{
    public static class JWTUtils
    {
        public static bool IsExpiredToken(this string token, DateTime now)
        {
            JwtSecurityToken jwt = new JwtSecurityToken(token);
            if (jwt.ValidTo < now) return true;
            return false;
        }
    }
}
