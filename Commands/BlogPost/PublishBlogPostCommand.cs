using FloraYFaunaAPI.Validations;
using System;

namespace FloraYFaunaAPI.Commands.BlogPost
{
    public class PublishBlogPostCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
