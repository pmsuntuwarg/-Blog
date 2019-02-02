using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entites
{
    public class BaseModel
    {
        [Required]
        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public int UpdatedBy { get; set; }

        [ForeignKey("UpdatedBy")]
        public ApplicationUser User { get; set; }
    }
}
