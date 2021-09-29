using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using FloraYFaunaAPI.Enums;
using FloraYFaunaAPI.Validations;

namespace FloraYFaunaAPI.Commands.Document
{
    public class CreateDocumentCommand
    {
        [Required(ErrorMessage = "Por favor, seleccione un archivo.")]
        [ValidationTypeDocument(ValidFile.Document)]
        [ValidationSizeDocument(1024)]
        public IFormFile File { get; set; }

        [Required(ErrorMessage = "Por favor, escriba un título para el documento.")]
        [ValidationNameDocument(120)]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
