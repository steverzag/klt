using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraYFaunaAPI.ViewModel
{
    public class BlogPostViewModel : BaseDBModel
    {
        public Guid Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int Likes { get; set; }
        public Guid CategoryId { get; set; }
    }
}
