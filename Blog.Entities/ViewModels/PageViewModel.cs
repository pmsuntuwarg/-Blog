using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Blog.Entities.ViewModels
{
    public class PageViewModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Page Name")]

        public string PageName { get; set; }

        [Required]
        [Display(Name = "Page Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Page Body")]
        public string Body { get; set; }

        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
    }
}
