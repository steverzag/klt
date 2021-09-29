using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FloraYFaunaAPI.Models
{
    [Table("Categories", Schema = "dbo")]
    public partial class Category : BaseDBModel
    {
        public Category()
        {
            Metadata = new Metadata();
            OnCreated();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public virtual List<BlogPost> BlogPost { get; set; }

        partial void OnCreated();
    }
}
