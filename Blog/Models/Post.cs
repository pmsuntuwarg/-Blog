using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Post
    {
        public string PostId { get; set; } = Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks.ToString();
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime PublishedAt { get; set; }
        public bool IsPublished { get; set; } = false;
        public long ViewCount { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
