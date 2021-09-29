using FloraYFaunaAPI.Enums;
using FloraYFaunaAPI.Validations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Commands.Carousel
{
    public class CreateCarouselCommand
    {
        [Required(ErrorMessage = "Por favor, seleccione un archivo.")]
        [ValidationTypeDocument(ValidFile.Image)]
        [ValidationSizeDocument(1024)]
        public IFormFile File { get; set; }

        [Required(ErrorMessage = "Por favor, escriba un título para mostrar en el Carousel.")]
        [ValidationNameDocument(120)]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Por favor, escriba una breve descripción para mostrar en el Carousel.")]
        [ValidationNameDocument(200)]
        public string Caption { get; set; }
    }
}
