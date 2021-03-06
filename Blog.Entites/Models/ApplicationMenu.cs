﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entites.Models
{
    public class ApplicationMenu : BaseModel
    {
        public string ParentMenuId { get; set; }

        [Required]
        public string MenuName { get; set; }
        
        [Required]
        public string MenuIconClass { get; set; }

        [Required]
        public string Area { get; set; }

        [Required]
        public string Controller { get; set; }

        [Required]
        public string Action { get; set; }

        public bool IsHidden { get; set; }

        [ForeignKey("ParentMenuId")]
        public ApplicationMenu ParentMenu { get; set; }
    }
}
