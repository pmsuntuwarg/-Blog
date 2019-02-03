using Blog.Entities.Models;
using Blog.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Entities.ViewModels
{
    public class PostViewModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Excerpt { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IsPublished { get; set; }

        [Display(Name = "Tags")]
        [Required(ErrorMessage = "{0} is required")]
        public IList<PostTag> PostTags { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "{0} is required")]
        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public DateTime CreatedDate { get; set; }
        public int ViewCount { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
