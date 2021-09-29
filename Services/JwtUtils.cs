using FloraYFaunaAPI.Models;
using FloraYFaunaAPI.Services.Contract;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FloraYFaunaAPI.Services
{
    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettingsModel _appSettings;

        public JwtUtils(IOptions<AppSettingsModel> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public Guid? ValidateJwtToken(string token)
        {
            if (token == null)
            {
                return null;
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                try
                {
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _appSettings.Jwt.ISSUER_TOKEN,
                        ValidAudience = _appSettings.Jwt.AUDIENCE_TOKEN,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt.SECRET_KEY))
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken) validatedToken;
                    var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "nameid").Value);
                    return userId;
                }
                catch
                {
                    return null;
                }
            }
        }

        public RefreshToken GenerateRefreshToken(Guid userId, string ipAddress)
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };

            return refreshToken;
        }


    }
}
