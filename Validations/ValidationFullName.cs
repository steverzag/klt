using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FloraYFaunaAPI.Validations
{
    public class ValidationFullName : ValidationAttribute
    {
        private string _varibleName;
        private int _minlenght;
        private int _maxlenght;

        public ValidationFullName(string varibleName, int minlenght, int maxlenght)
        {
            _varibleName = varibleName;
            _minlenght = minlenght;
            _maxlenght = maxlenght;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var cadena = value as string;
            if (cadena.Length > 0)
            {
                if (cadena.Length < _minlenght)
                {
                    return new ValidationResult($"El campo {_varibleName} tiene que ser mayor que {_minlenght} caracteres");
                }

                if (_maxlenght > cadena.Length)
                {
                    return new ValidationResult($"El campo {_varibleName} excede el máximo de {_maxlenght} caracteres permitidos");
                }

                Regex Val = new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúñÑ ]+$");
                if (!Val.IsMatch(cadena))
                {
                    return new ValidationResult($"El El campo {_varibleName} no es válido");
                }
            }
            return ValidationResult.Success;
        }
    }
}
