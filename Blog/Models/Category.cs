using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    [Table(name:"Categories")]
    public class Category
    {
        [Display(Name = "Id")]
        public string CategoryId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}
