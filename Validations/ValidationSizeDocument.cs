using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Validations
{
    public class ValidationSizeDocument : ValidationAttribute 
    {
        private readonly double _size;

        public ValidationSizeDocument(double size)
        {
            _size = size;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var formFile = value as IFormFile;
            if (formFile != null)
            {
                if (formFile.Length/1024 > _size)
                {
                    return new ValidationResult($"El máximo de tamaño permitido para subir un archivos es de {_size}");
                }
            }
            return ValidationResult.Success;
        }

    }
}
