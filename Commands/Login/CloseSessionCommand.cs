using FloraYFaunaAPI.Validations;
using System;

namespace FloraYFaunaAPI.Commands.Login
{
    public class CloseSessionCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
