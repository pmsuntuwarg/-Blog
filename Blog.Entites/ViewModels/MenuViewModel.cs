using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entites.ViewModels
{
    public class MenuViewModel
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "Parent Menu is Required")]
        [Display(Name = "Parent Menu")]
        public string ParentMenuId { get; set; }

        [Required(ErrorMessage = "Menu Icon Class is Required")]
        [Display(Name = "Menu Icon Class")]
        public string MenuIconClass { get; set; }

        [Required]
        public string Area { get; set; }

        [Required]
        public string Controller { get; set; }

        [Required]
        public string Action { get; set; }
    }
}
