using FloraYFaunaAPI.Validations;
using System;

namespace FloraYFaunaAPI.Commands.Carousel
{
    public class DisabledEnabledCarouselCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
