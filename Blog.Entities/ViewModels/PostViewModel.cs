using Blog.Common.Resources;
using Blog.Common.Resources.BlogResource;
using Blog.Entities.Models;
using Blog.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Entities.ViewModels
{
    [Display(ResourceType = typeof(BlogResource))]
    public class PostViewModel
    { 
        [Key]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Title"/*typeof(BlogResource.Title)*/, ResourceType = typeof(BlogResource))]
        public string Title { get; set; }

        [Required]
        public string Excerpt { get; set; }
        public string Slug { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IsPublished { get; set; }

        [Display(Name = "Tags")]
        [Required(ErrorMessage = "{0} is required")]
        public List<string> TagIds { get; set; } = new List<string>();

        [Display(Name = "Category")]
        [Required(ErrorMessage = "{0} is required")]
        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public DateTime CreatedDate { get; set; }
        public long ViewCount { get; set; }
        public int CommentCount { get; set; }
        public string Author { get; set; }

        public IList<PostTag> PostTags { get; set; } 
        public IReadOnlyList<CategoryViewModel> Categories { get; set; } 
        public IReadOnlyList<Tag> Tags { get; set; } 
        public IReadOnlyList<CommentViewModel> Comments { get; set; } 
    }
}
