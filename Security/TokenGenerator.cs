using System;
using System.Security.Claims;
using FloraYFaunaAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace FloraYFaunaAPI.Context
{
    internal static class TokenGenerator
    {
        public static string GenerateTokenJwt(User user, AppSettingsModel appSettings)
        {
            var secretKey = appSettings.Jwt.SECRET_KEY;
            var audienceToken = appSettings.Jwt.AUDIENCE_TOKEN;
            var issuerToken = appSettings.Jwt.ISSUER_TOKEN;
            var expireTime = appSettings.Jwt.EXPIRE_HOURS;

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Rol.ToString()),
            });

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }
    }
}
