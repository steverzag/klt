using FloraYFaunaAPI.Validations;
using System;

namespace FloraYFaunaAPI.Commands.BlogPost
{
    public class LikeBlogPostCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
