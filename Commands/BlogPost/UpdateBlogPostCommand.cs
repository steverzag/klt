using FloraYFaunaAPI.Validations;
using System;
using System.ComponentModel.DataAnnotations;


namespace FloraYFaunaAPI.Commands.BlogPost
{
    public class UpdateBlogPostCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Por favor, escriba un título para el post.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Por favor, escriba un descripción para el post.")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Por favor, escriba el nombre del autor.")]
        public string Author { get; set; }

        [ValidationGuid("categoria")]
        public Guid CategoryId { get; set; }
    }
}
