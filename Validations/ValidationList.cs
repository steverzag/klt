using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Validations
{
    public class ValidationList : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = value as IList<Guid>;
            if(list.Count <= 0)
            {
                return new ValidationResult("El listado no puede ser vacío");
            }
            return ValidationResult.Success;
        }
    }
}
