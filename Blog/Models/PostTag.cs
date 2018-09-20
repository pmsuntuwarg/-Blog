using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    [Table("PostTags")]
    public class PostTag
    {
        public string PostId { get; set; }
        public string TagId { get; set; }
        [ForeignKey("PostId")]
        public Post Posts { get; set; }
        [ForeignKey("TagId")]
        public Tag Tags { get; set; }
    }
}
