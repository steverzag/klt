using System;
using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Commands.Category
{
    public class ReadCategoryCommand
    {
        [Required(ErrorMessage = "El identificador es requerido.")]
        public Guid Id { get; set; }
    }
}
