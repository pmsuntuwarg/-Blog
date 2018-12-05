using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.Models
{
    [Table("Comments")]
    public class Comment
    {
        public string CommentId { get; set; } = DateTime.UtcNow.Ticks.ToString() + Guid.NewGuid().ToString();
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public bool IsReply { get; set; } = false;

        public string ParentId { get; set; }
        public string PostId { get; set; }

        [ForeignKey("ParentId")]
        public Comment Comments { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
