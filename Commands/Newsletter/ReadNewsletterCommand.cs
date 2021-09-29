using System;
using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Commands.Newsletter
{
    public class ReadNewsletterCommand
    {
        [Required(ErrorMessage = "El identificador es requerido.")]
        public Guid Id { get; set; }
    }
}
