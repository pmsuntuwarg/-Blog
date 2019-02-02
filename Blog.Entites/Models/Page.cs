using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.Models
{
    public class Page
    {
        public string PageId { get; set; }
        public string PageName { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string PageSlug { get; set; }
        public bool isPublished { get; set; }
        public bool isDraft { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
