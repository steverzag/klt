using FloraYFaunaAPI.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Commands.Document
{
    public class UpdateDocumentCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Por favor, escriba un título para el documento.")]
        [ValidationNameDocument(120)]
        public string Title { get; set; }

        public string Description { get; set; }

    }
}
