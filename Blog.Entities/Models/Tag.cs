using System.Collections.Generic;

namespace Blog.Entities.Models
{
    public class Tag : BaseModel
    {
        public string Name { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
