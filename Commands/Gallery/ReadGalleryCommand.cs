using FloraYFaunaAPI.Validations;
using System;

namespace FloraYFaunaAPI.Commands.Gallery
{
    public class ReadGalleryCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
