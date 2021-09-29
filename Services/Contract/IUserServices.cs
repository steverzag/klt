using FloraYFaunaAPI.Models;
using System;
using System.Collections.Generic;

namespace FloraYFaunaAPI.Services.Contract
{
    public interface IUserServices
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
        AuthenticateResponse Authenticated(User model, string jwtToken, string ipAddress);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
        void RevokeToken(string token, string ipAddress);
        IEnumerable<User> GetAll();
        User GetById(Guid id);
    }
}
