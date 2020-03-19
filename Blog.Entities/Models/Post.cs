using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Entities.Models
{
    public class Post : BaseModel
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public string PostTitleImgUrl { get; set; }
        public DateTime PublishedAt { get; set; }
        public bool IsPublished { get; set; } = false;
        public long ViewCount { get; set; }
        public string TitleImageId { get; set; }
        public string CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("TitleImageId")]
        public Media Media { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
        public IList<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}
