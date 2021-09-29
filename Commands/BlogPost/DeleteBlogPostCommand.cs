using FloraYFaunaAPI.Validations;
using System;

namespace FloraYFaunaAPI.Commands.BlogPost
{
    public class DeleteBlogPostCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
