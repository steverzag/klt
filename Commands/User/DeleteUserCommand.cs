using System;
using FloraYFaunaAPI.Validations;

namespace FloraYFaunaAPI.Commands.User
{
    public class DeleteUserCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
