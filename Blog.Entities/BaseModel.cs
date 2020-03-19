using Blog.Entities.Models.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Entities
{
    public class BaseModel
    {
        [Key]
        public string Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }

        [ForeignKey("CreatedById")]
        public ApplicationUser CreatedBy { get; set; }

        [ForeignKey("UpdatedById")]
        public ApplicationUser UpdatedBy { get; set; }

        [NotMapped]
        public int TotalCount { get; set; }

        [NotMapped]
        public int FilteredCount { get; set; }
    }
}
