using System;
using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Validations
{
    public class ValidationGuid : ValidationAttribute
    {
        private string _otherGuid;

        public ValidationGuid()
        {

        }

        public ValidationGuid(string otherGuid)
        {
            _otherGuid = otherGuid;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            if (value == null || Guid.Empty == (Guid) value )
            {
                if(_otherGuid != null && !_otherGuid.Equals(""))
                {
                    return new ValidationResult($"El campo {_otherGuid} es requerido.");
                }
                else
                {
                    return new ValidationResult($"El identificador es requerido.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
