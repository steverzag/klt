using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraYFaunaAPI.ViewModel
{
    public class DocumentViewModel : BaseDBModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string MimeType { get; set; }
    }
}
