using Bogus;
using FloraYFaunaAPI.Models;
using System;

namespace FloraYFaunaAPI.FakeData
{
    public class FakeMetadata
    {
        public static Metadata GenerateData(Guid createdBy, Guid updatedBy)
        {
            Faker<Metadata> metadata = new Faker<Metadata>()
                .RuleFor(m => m.CreatedAt, f => f.Date.Past())
                .RuleFor(m => m.UpdatedAt, f => f.Date.Future())
                .RuleFor(m => m.IsDeleted, f => f.PickRandomParam(new bool[] { false, false, false, false, true }))
                .RuleFor(m => m.CreatedBy, f => createdBy)
                .RuleFor(m => m.UpdatedBy, f => updatedBy);
            return metadata.Generate();
        }
    }
}
