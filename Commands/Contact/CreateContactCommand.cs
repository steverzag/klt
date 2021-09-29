using FloraYFaunaAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Commands.Contact
{
    public class CreateContactCommand
    {
        [Required(ErrorMessage = "Por favor, el campo nombre es requerido.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Por favor, el campo email es requerido.")]
        [ValidationEmail("email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Por favor, el campo asunto es requerido.")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Por favor, el campo mensaje es requerido.")]
        public string Message { get; set; }
    }
}
