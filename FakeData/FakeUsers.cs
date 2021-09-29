using Bogus;
using FloraYFaunaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FloraYFaunaAPI.FakeData
{
    public class FakeUsers
    {
        public static List<User> GenerateData(FakeParams fakeParams, byte[] passwordHash, byte[] passwordSalt)
        {
            Faker<User> testUsers = new Faker<User>()
                .RuleFor(u => u.UserId, f => Guid.NewGuid())
                .RuleFor(u => u.FullName, "Admin")
                .RuleFor(u => u.Username, "Admin")
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.PasswordHash, passwordHash)
                .RuleFor(u => u.PasswordSalt, passwordSalt)
                .RuleFor(u => u.Rol, Enums.UserRol.SuperAdmin)
                .RuleFor(u => u.Enabled, true)
                .RuleFor(u => u.Metadata, f => FakeMetadata.GenerateData(fakeParams.CreatedBy, fakeParams.UpdatedBy));
     
            List<User> users = testUsers.Generate(fakeParams.Count).ToList();
            return users;
        }
    }
}
