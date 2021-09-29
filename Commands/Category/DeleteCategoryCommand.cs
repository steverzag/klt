using System;
using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Commands.Category
{
    public class DeleteCategoryCommand
    {
        [Required(ErrorMessage = "El identificador es requerido.")]
        public Guid Id { get; set; }
    }
}
