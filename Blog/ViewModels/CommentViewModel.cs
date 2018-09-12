using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class CommentViewModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string ParentId { get; set; }
        public string PostId { get; set; }
    }
}
