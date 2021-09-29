using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FloraYFaunaAPI.Models
{
    [Table("Documents", Schema = "dbo")]
    public partial class Document : BaseDBModel
    {
        public Document()
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

        [Required]
        public virtual string FileName { get; set; }

        [Required]
        public virtual string Extension { get; set; }

        [Required]
        public virtual string MimeType { get; set; }

        partial void OnCreated();
    }
}
