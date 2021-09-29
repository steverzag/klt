using FloraYFaunaAPI.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Commands.Carousel
{
    public class UpdateCarouselCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Por favor, escriba un título para mostrar en el Carousel.")]
        [ValidationNameDocument(120)]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Por favor, escriba una breve descripción para mostrar en el Carousel.")]
        [ValidationNameDocument(200)]
        public string Caption { get; set; }
    }
}
