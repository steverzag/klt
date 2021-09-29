using System;
using System.ComponentModel.DataAnnotations;
using FloraYFaunaAPI.Validations;

namespace FloraYFaunaAPI.Commands.Command.Gallery
{
    public class UpdateGalleryCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Por favor, escriba un nombre para el archivo.")]
        [ValidationNameDocument(120)]
        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }
    }
}
