using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FloraYFaunaAPI.Enums;

namespace FloraYFaunaAPI.Validations
{
    public class ValidationUserRol : ValidationAttribute
    {
        private readonly List<int> _roles = new List<int>();

        public ValidationUserRol()
        {
            _roles = new List<int>();
            foreach (int rol in Enum.GetValues(typeof(UserRol)))
            {
                _roles.Add(rol);
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            UserRol rol = (UserRol)value;
            if(rol != 0)
            {
                if (!_roles.Contains((int)value) || UserRol.SuperAdmin.Equals((int)value)) ;
                {
                    return new ValidationResult($"El rol seleccionado { value } no es válido");
                }
            }
            return ValidationResult.Success;
        }
    }
}
