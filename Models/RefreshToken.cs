using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FloraYFaunaAPI.Models
{
    [Table("RefreshToken", Schema = "dbo")]
    public partial class RefreshToken
    {
        public RefreshToken()
        {
            OnCreated();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual string Token { get; set; }
        public User User { get; set; }

        public virtual DateTime Expires { get; set; }
        public virtual DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public virtual DateTime? Revoked { get; set; }

        public virtual string RevokedByIp { get; set; }
        public virtual string ReplacedByToken { get; set; }
        public virtual string ReasonRevoked { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsRevoked => Revoked != null;
        public bool IsActive => !IsRevoked && !IsExpired;

        partial void OnCreated();
    }
}
