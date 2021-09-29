using FloraYFaunaAPI.FakeData;
using FloraYFaunaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FloraYFaunaAPI.Context
{
    public class DemoDataGenerator
    {
        public static void CleanDatabase(ApplicationDbContext dBContext)
        {
            List<string> tableNames = dBContext.Model.GetEntityTypes().Select(t => t.GetTableName()).Distinct().ToList();

            foreach (string table in tableNames)
            {
                dBContext.Database.ExecuteSqlRaw($"delete from {table}");
            }
        }

        public static void SeedData(ApplicationDbContext dBContext)
        {
            byte[] passwordHash, passwordSalt;
            GetHash("admin", out passwordHash, out passwordSalt);
            Guid userBy = Guid.NewGuid();
            List<User> users = dBContext.Users.ToListAsync().Result;
            if (users.Count == 0)
            {
                users = FakeUsers.GenerateData(new FakeParams
                {
                    Count = Constants.UsersCount,
                    CreatedBy = userBy,
                    UpdatedBy = userBy
                },passwordHash,passwordSalt);

                dBContext.Users.AddRange(users);
            }

            List<Category> categories = dBContext.Categories.ToListAsync().Result;
            if (categories.Count == 0)
            {
                categories = FakeCategories.GenerateData(new FakeParams {
                    Count = Constants.CategoriesCount, 
                    CreatedBy = userBy,
                    UpdatedBy = userBy
                });
                dBContext.Categories.AddRange(categories);
            }

            dBContext.SaveChangesAsync();
        }

        public static void GetHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }

    internal static class Constants
    {
        public const int UsersCount = 1;
        public const int CategoriesCount = 5;
    }
}
