using FloraYFaunaAPI.Validations;
using System;

namespace FloraYFaunaAPI.Commands.User
{
    public class DisabledEnabledUserCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
