using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FloraYFaunaAPI.Models
{
    [Table("Gallery", Schema = "dbo")]
    public partial class Gallery : BaseDBModel
    {
        public Gallery()
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

        [StringLength(600)]
        public virtual string Description { get; set; }

        [StringLength(60)]
        public virtual string Author { get; set; }

        [Required]
        public virtual string FileName { get; set; }

        [Required]
        public virtual string Extension { get; set; }

        partial void OnCreated();
    }
}
