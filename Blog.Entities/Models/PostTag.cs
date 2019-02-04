using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Entities.Models
{
    public class PostTag
    {
        public string PostId { get; set; }

        public string TagId { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }

        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
