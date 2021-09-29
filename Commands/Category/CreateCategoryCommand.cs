using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Commands.Category
{
    public class CreateCategoryCommand
    {
        [Required(ErrorMessage = "Por favor, escriba un nombre para la categoria.")]
        public string Name { get; set; }
    }
}
