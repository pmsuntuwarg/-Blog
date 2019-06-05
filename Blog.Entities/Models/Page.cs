using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Models
{
    public class Page : BaseModel
    {
        public string PageName { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string PageSlug { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDraft { get; set; }

        [NotMapped]
        public int TotalCount { get; set; }

        [NotMapped]
        public int FilteredCount { get; set; }
    }
}
