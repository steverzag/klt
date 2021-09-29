using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FloraYFaunaAPI.Models
{
    [Table("Newsletters", Schema = "dbo")]
    public partial class Newsletter : BaseDBModel
    {
        public Newsletter()
        {
            Metadata = new Metadata();
            OnCreated();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        [MaxLength(60)]
        public virtual string FullName { get; set; }

        [Required]
        [EmailAddress]
        public virtual string Email { get; set; }

        partial void OnCreated();
    }
}
