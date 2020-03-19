using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Entities.ViewModels
{
    public class MediaViewModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Page Name")]
        public string FileName { get; set; }

        [Required]
        [Display(Name = "Upload File")]
        public IFormFile UploadedFile { get; set; }

        public string PreviewUrl { get; set; }

        public string FileUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
    }
}
