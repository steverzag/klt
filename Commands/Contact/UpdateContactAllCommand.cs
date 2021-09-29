using FloraYFaunaAPI.Validations;
using System;
using System.Collections.Generic;

namespace FloraYFaunaAPI.Commands.Contact
{
    public class UpdateContactAllCommand
    {
        [ValidationList]
        public List<Guid> list { get; set; }
    }
}
