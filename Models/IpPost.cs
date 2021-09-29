using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FloraYFaunaAPI.Models
{
    [Table("IpPosts", Schema = "dbo")]
    public partial class IpPost
    {
        public IpPost()
        {
            OnCreated();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        public virtual Guid BlogPostId { get; set; }

        public virtual BlogPost BlogPost { get; set; }

        public string IpAddress { get; set; }

        partial void OnCreated();
    }
}
