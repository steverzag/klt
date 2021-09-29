using Bogus;
using FloraYFaunaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FloraYFaunaAPI.FakeData
{
    public class FakeCategories
    {
        public static List<Category> GenerateData(FakeParams fakeParams)
        {
            Faker<Category> testCategories = new Faker<Category>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Name, f =>f.PickRandom(f.Lorem.Word()))
                .RuleFor(u => u.Metadata, f => FakeMetadata.GenerateData(fakeParams.CreatedBy, fakeParams.UpdatedBy));

            List<Category> categories = testCategories.Generate(fakeParams.Count).ToList();
            return categories;
        }
    }
}
