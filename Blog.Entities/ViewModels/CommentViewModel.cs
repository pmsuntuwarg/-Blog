using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Entities.ViewModels
{
    public class CommentViewModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ParentId { get; set; }
        public string PostId { get; set; }
    }
}
