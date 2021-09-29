using FloraYFaunaAPI.Validations;
using System;

namespace FloraYFaunaAPI.Commands.BlogPost
{
    public class ReadBlogPostCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
