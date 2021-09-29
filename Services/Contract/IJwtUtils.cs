using FloraYFaunaAPI.Models;
using System;

namespace FloraYFaunaAPI.Services.Contract
{
    public interface IJwtUtils
    {
        public Guid? ValidateJwtToken(string token);
        public RefreshToken GenerateRefreshToken(Guid userId,string ipAddress);
    }
}
