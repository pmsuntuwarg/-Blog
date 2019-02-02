using Blog.Entities.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Models
{
    public class Comment : BaseModel
    {
        public string CommenterId { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;

        public string ParentId { get; set; }
        public string PostId { get; set; }

        [ForeignKey("CommenterId")]
        public ApplicationUser Commenter { get; set; }

        [ForeignKey("ParentId")]
        public Comment Comments { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
