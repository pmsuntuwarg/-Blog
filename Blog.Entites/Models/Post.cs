using Blog.Areas.Admin.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Areas.Admin.Models
{
    public class Post
    {
        public string PostId { get; set; } = Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks.ToString();
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public string PostTitleImgUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime PublishedAt { get; set; }
        public bool IsPublished { get; set; } = false;
        public long ViewCount { get; set; }
        public string CategoryId { get; set; }
        public string AutherId { get; set; }

        [ForeignKey("AutherId")]
        public User Auther { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
        public List<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}
