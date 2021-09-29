using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraYFaunaAPI.ViewModel
{
    public class MetadataViewModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public Guid UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
