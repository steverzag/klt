using FloraYFaunaAPI.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Commands.User
{
    public class SetPasswordUserCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        public string NewPassword { get; set; }
    }
}
