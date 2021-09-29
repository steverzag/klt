using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Commands.Login
{
    public class RevokeTokenCommand
    {
        [Required(ErrorMessage = "El token de usuario es requerido.")]
        public string Token { get; set; }
    }
}
