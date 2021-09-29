using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using FloraYFaunaAPI.Enums;
using FloraYFaunaAPI.Validations;

namespace FloraYFaunaAPI.Commands.Command.Gallery
{
    public class CreateGalleryCommand
    {
        [Required(ErrorMessage = "Por favor, seleccione un archivo.")]
        [ValidationTypeDocument(ValidFile.Image)]
        [ValidationSizeDocument(1024)]
        public IFormFile File { get; set; }

        [Required(ErrorMessage = "Por favor, escriba un nombre para el archivo.")]
        [ValidationNameDocument(120)]
        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

    }
}
