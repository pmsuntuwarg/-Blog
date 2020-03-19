using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Entities.Models
{
    public class Category : BaseModel
    {
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}
