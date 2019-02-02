using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        public string Id { get; set; }
        public string CategoryName { get; set; }
    }
}
