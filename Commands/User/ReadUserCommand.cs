using FloraYFaunaAPI.Validations;
using System;

namespace FloraYFaunaAPI.Commands.User
{
    public class ReadUserCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
