using System.ComponentModel.DataAnnotations;

namespace Blog.Entities.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        public string Id { get; set; }
        public string CategoryName { get; set; }
    }
}
