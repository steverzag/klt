using FloraYFaunaAPI.Validations;
using System;

namespace FloraYFaunaAPI.Commands.Carousel
{
    public class DeleteCarouselCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
