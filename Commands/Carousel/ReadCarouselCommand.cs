using FloraYFaunaAPI.Validations;
using System;

namespace FloraYFaunaAPI.Commands.Carousel
{
    public class ReadCarouselCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
