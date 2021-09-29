using FloraYFaunaAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Commands.Newsletter
{
    public class CreateNewsletterCommand
    {
        [Required(ErrorMessage = "Por favor, escriba su nombre.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Por favor, escriba su email.")]
        [ValidationEmail("email")]
        public string Email { get; set; }
    }
}
