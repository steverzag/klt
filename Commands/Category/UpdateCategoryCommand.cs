using System;
using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Commands.Category
{
    public class UpdateCategoryCommand
    {
        [Required(ErrorMessage = "El identificador es requerido.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Por favor, escriba un nombre para la categoria.")]
        public string Name { get; set; }

    }
}
