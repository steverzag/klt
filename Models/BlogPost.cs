using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FloraYFaunaAPI.Models
{
    [Table("BlogPosts", Schema = "dbo")]
    public partial class BlogPost : BaseDBModel
    {
        public BlogPost()
        {
            Metadata = new Metadata();
            OnCreated();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        [Required]
        public virtual string Slug { get; set; }

        [Required]
        [StringLength(100)]
        public virtual string Title { get; set; }

        [Required]
        [StringLength(2500)]
        public virtual string Description { get; set; }

        [Required]
        [StringLength(100)]
        public virtual string Author { get; set; }

        [Required]
        public virtual string FileName { get; set; }

        [Required]
        public virtual string Extension { get; set; }

        public virtual int Likes { get; set; }

        public virtual DateTime PublishDate { get; set; }

        public virtual Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual bool Enabled { get; set; }

        public virtual List<IpPost> IpPost { get; set; }

        partial void OnCreated();
    }
}
