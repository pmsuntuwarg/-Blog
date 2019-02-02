using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.ViewModels
{
    public class CommentViewModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Content { get; set; }
        public string ParentId { get; set; }
        public string PostId { get; set; }
    }
}
