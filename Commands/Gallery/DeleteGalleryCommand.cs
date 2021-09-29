using FloraYFaunaAPI.Validations;
using System;

namespace FloraYFaunaAPI.Commands.Command.Gallery
{
    public class DeleteGalleryCommand
    {        
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
