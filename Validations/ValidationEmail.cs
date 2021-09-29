using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace FloraYFaunaAPI.Validations
{
    public class ValidationEmail : ValidationAttribute
    {
        private string _varibleName;

        public ValidationEmail(string varibleName)
        {
            _varibleName = varibleName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var email = value as string;
            if (email != null && !email.Equals(""))
            {
                var response = Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                return response == true ? ValidationResult.Success : new ValidationResult($"El campo {_varibleName} no es válido");
            }
            return ValidationResult.Success;
        }
    }
}
