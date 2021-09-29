using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FloraYFaunaAPI.Validations
{
    public class ValidationNameDocument : ValidationAttribute
    {
        private int _lenght;
        public ValidationNameDocument(int lenght)
        {
            _lenght = lenght;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var name = value as string;
            if (name != null && !name.Equals(""))
            {
                if(name.Length > _lenght)
                {
                    return new ValidationResult($"El nombre del documento excede el máximo de {_lenght} caracteres permitidos");
                }

                Regex Val = new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúñÑ ]+$");
                if (!Val.IsMatch(name))
                {
                    return new ValidationResult($"El nombre del documento no es válido");
                }
            }
            return ValidationResult.Success;
        }
    }
}
