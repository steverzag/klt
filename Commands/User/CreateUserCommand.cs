using FloraYFaunaAPI.Validations;
using System.ComponentModel.DataAnnotations;
using FloraYFaunaAPI.Enums;
using System.Text.Json.Serialization;

namespace FloraYFaunaAPI.Commands.User
{
    public class CreateUserCommand
    {

        [Required(ErrorMessage = "Por favor, el campo nombre y apellidos. es requeridos")]
        [ValidationFullName("nombre y apellidos", 5, 60)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Por favor, el campo username es requerido.")]
        [ValidationUserName("username", 4, 12)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Por favor, el campo email es requerido.")]
        [ValidationEmail("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, seleccione un rol para el usuario.")]
        [ValidationUserRol]
        public UserRol Rol { get; set; }

        [Required(ErrorMessage = "Por favor, el campo password es requerido.")]
        [ValidationPassword("contraseña",6)]
        public string Password { get; set; }
    }
}
