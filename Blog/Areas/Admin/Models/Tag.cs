using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.Models
{
    [Table(name:"Tags")]
    public class Tag
    {
        public string TagId { get; set; }
        public string TagName { get; set; }

        public List<PostTag> PostTags { get; } = new List<PostTag>();
    }
}
