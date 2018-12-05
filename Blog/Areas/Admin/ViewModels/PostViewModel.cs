using System.Collections.Generic;

namespace Blog.Areas.Admin.ViewModels
{
    public class PostViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public List<string> PostTags { get; set; }
        public string Category { get; set; }
    }
}
