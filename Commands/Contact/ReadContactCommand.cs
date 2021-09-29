using FloraYFaunaAPI.Validations;
using System;

namespace FloraYFaunaAPI.Commands.Contact
{
    public class ReadContactCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
