using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FloraYFaunaAPI.Enums;

namespace FloraYFaunaAPI.Models
{
    [Table("Users", Schema = "dbo")]
    public partial class User : BaseDBModel
    {
        public User()
        {
            Metadata = new Metadata();
            OnCreated();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid UserId { get; set; }

        [Required]
        public virtual string FullName { get; set; }

        [Required]
        public virtual string Username { get; set; }

        [Required]
        [EmailAddress]
        public virtual string Email { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public virtual UserRol Rol { get; set; }

        public virtual bool Enabled { get; set; }

        public List<RefreshToken> RefreshToken { get; set; }

        partial void OnCreated();
    }
}
