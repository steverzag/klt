using FloraYFaunaAPI.Context;
using FloraYFaunaAPI.Exceptions;
using FloraYFaunaAPI.Models;
using FloraYFaunaAPI.Services.Contract;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FloraYFaunaAPI.Services
{
    public class UserServices : IUserServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly AppSettingsModel _appSettings;

        public UserServices(ApplicationDbContext context, IJwtUtils jwtUtils, IOptions<AppSettingsModel> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _jwtUtils = jwtUtils ?? throw new ArgumentNullException(nameof(jwtUtils));
        }

        public AuthenticateResponse Authenticated(User model, string jwtToken, string ipAddress)
        {
            var refreshToken = _jwtUtils.GenerateRefreshToken(model.UserId,ipAddress);
            _context.RefreshToken.Add(refreshToken);
            removeOldRefreshTokens(model);
            _context.SaveChangesAsync();
            return new AuthenticateResponse(model, jwtToken, refreshToken.Token);
        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            var user = getUserByRefreshToken(token);
            var refreshToken = user.RefreshToken.Single(x => x.Token == token);
            if (refreshToken.IsRevoked)
            {
                revokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Intento de reutilización del token de ancestro revocado: {token}");
                _context.Update(user);
                _context.SaveChanges();
            }
            if (!refreshToken.IsActive)
                throw new AppException("Token inválido");
            var newRefreshToken = rotateRefreshToken(refreshToken, ipAddress);
            user.RefreshToken.Add(newRefreshToken);
            removeOldRefreshTokens(user);
            _context.Update(user);
            _context.SaveChangesAsync();
            var jwtToken = TokenGenerator.GenerateTokenJwt(user, _appSettings);
            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("Userio no encontrado");
            return user;
        }

        public void RevokeToken(string token, string ipAddress)
        {
            var user = getUserByRefreshToken(token);
            var refreshToken = user.RefreshToken.Single(x => x.Token == token);
            if (!refreshToken.IsActive)
                throw new AppException("Token inválido");

            revokeRefreshToken(refreshToken, ipAddress, "Revocado sin reemplazo");
            _context.Update(user);
            _context.SaveChangesAsync();
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }

        private User getUserByRefreshToken(string token)
        {
            var user = _context.Users.SingleOrDefault(u => u.RefreshToken.Any(t => t.Token == token));
            if (user == null)
                throw new AppException("Token Inválido");
            return user;
        }

        private RefreshToken rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtUtils.GenerateRefreshToken(refreshToken.UserId,ipAddress);
            revokeRefreshToken(refreshToken, ipAddress, "Reemplazado por nuevo token", newRefreshToken.Token);
            return newRefreshToken;
        }

        private void removeOldRefreshTokens(User user)
        {
            var list = _context.RefreshToken.Where(x => x.UserId.Equals(user.UserId)).ToList(); 
            list = list.Where(x => x.IsActive &&
                x.Created.AddHours(_appSettings.Jwt.EXPIRE_HOURS) <= DateTime.UtcNow).ToList();
            foreach (var x in list)
            {
                _context.RefreshToken.Remove(x);
            }
        }

        private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }

        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
        {
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshToken.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    revokeRefreshToken(childToken, ipAddress, reason);
                else
                    revokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
            }
        }
    }
}
