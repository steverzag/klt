using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FloraYFaunaAPI.Models
{
    [Table("Contacts", Schema = "dbo")]
    public partial class Contact : BaseDBModel
    {
        public Contact()
        {
            Metadata = new Metadata();
            OnCreated();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [Required]
        [EmailAddress]
        public virtual string Email { get; set; }
        [Required]
        [StringLength(100)]
        public virtual string Subject { get; set; }
        [Required]
        [StringLength(400)]
        public virtual string Message { get; set; }

        public virtual bool MarkAsRead { get; set; }

        partial void OnCreated();
    }
}
