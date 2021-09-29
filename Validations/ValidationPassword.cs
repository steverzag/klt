using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FloraYFaunaAPI.Validations
{
    public class ValidationPassword : ValidationAttribute
    {
        private string _varibleName;
        private int _minlenght;

        public ValidationPassword(string varibleName, int minlenght)
        {
            _varibleName = varibleName;
            _minlenght = minlenght;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;
            if (password.Length > 0)
            {
                Regex Val = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{"+_minlenght+",15}$");
                if (!Val.IsMatch(password))
                {
                    return new ValidationResult($"El El campo {_varibleName} no es válido");
                }
            }
            return ValidationResult.Success;
        }
    }
}
