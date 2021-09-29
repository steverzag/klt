using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Models
{
    [Owned]
    public partial class Metadata
    {
        public Metadata()
        {
            OnCreated();
        }

        [Required] public virtual DateTime CreatedAt { get; set; }

        [Required] public virtual DateTime UpdatedAt { get; set; }

        [Required] public virtual Guid CreatedBy { get; set; }

        [Required] public virtual Guid UpdatedBy { get; set; }

        [Required] public virtual bool IsDeleted { get; set; }

        partial void OnCreated();

    }
}
