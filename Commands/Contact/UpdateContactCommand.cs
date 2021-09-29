using FloraYFaunaAPI.Validations;
using System;

namespace FloraYFaunaAPI.Commands.Contact
{
    public class UpdateContactCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
