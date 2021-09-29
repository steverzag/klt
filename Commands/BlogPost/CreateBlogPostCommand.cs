using FloraYFaunaAPI.Enums;
using FloraYFaunaAPI.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Commands.BlogPost
{
    public class CreateBlogPostCommand
    {
        [Required(ErrorMessage = "Por favor, seleccione un archivo.")]
        [ValidationTypeDocument(ValidFile.Image)]
        [ValidationSizeDocument(1024)]
        public IFormFile File { get; set; }

        [Required(ErrorMessage = "Por favor, escriba un título para el post.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Por favor, escriba un descripción para el post.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Por favor, escriba el nombre del autor.")]
        public virtual string Author { get; set; }

        [ValidationGuid("categoria")]
        public Guid CategoryId { get; set; }
    }
}
