using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FloraYFaunaAPI.Models
{
    [Table("Carousel", Schema = "dbo")]
    public partial class Carousel : BaseDBModel
    {
        public Carousel()
        {
            Metadata = new Metadata();
            OnCreated();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public virtual string Title { get; set; }

        [Required]
        [StringLength(200)]
        public virtual string Caption { get; set; }

        [Required]
        public virtual string FileName { get; set; }

        [Required]
        public virtual string Extension { get; set; }

        public virtual bool Enabled { get; set; }

        partial void OnCreated();
    }
}
