using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FloraYFaunaAPI.Context
{
    public class TokenUser
    {
        private List<Claim> Claims { get; }
        public Guid UserId { get; set; }
        public string DisplayRol { get; set; }       
        public string Username { get; set; }       

        public TokenUser(ClaimsPrincipal user)
        {
            if(user.Claims.Count() > 0)
            {
                Claims = user.Claims.ToList();
                try
                {
                    UserId = Guid.Parse(GetClaimValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier") as string);
                    Username = GetClaimValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name") as string;
                    DisplayRol = GetClaimValue("http://schemas.microsoft.com/ws/2008/06/identity/claims/role") as string;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
            
        }

        private object GetClaimValue(string typeName, string defaultValue = "")
        {
            try
            {
                return Claims.First(x => x.Type == typeName).Value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}