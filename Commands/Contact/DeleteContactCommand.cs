using FloraYFaunaAPI.Validations;
using System;

namespace FloraYFaunaAPI.Commands.Contact
{
    public class DeleteContactCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
