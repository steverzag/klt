using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FloraYFaunaAPI.Enums;

namespace FloraYFaunaAPI.Validations
{
    public class ValidationTypeDocument : ValidationAttribute
    {
        private readonly string[] _ext;

        public ValidationTypeDocument(string[] ext)
        {
            _ext = ext;
        }

        public ValidationTypeDocument(ValidFile validFile)
        {
            if(validFile == ValidFile.Image)
            {
                _ext = new[] { "image/png", "image/jpeg" };
            }
            else if(validFile == ValidFile.Document)
            {
                _ext = new[] { "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/pdf" };
            }

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var formFile = value as IFormFile;
            if(formFile != null)
            {
                if (! _ext.Contains(formFile.ContentType))
                {
                    return new ValidationResult($"Los tipos de archivos permitidos son {string.Join(",",_ext)}");
                }
            }
            return ValidationResult.Success;
        }
    }
}
