using Blog.Entities.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Entities.Models
{
    public class Tag : BaseModel
    {
        public string Name { get; set; }

        public List<PostTag> PostTags { get; }
    }
}
