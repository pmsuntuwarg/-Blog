using Blog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services
{
    public interface ICategoryService
    {
        Category GetCategoryById(string categoryId);
        int SaveCategory(Category category, string action="Create");
        int DeleteCategory(Category category);
        IEnumerable<Category> GetAllCategories();
        List<SelectListItem> GetSelectListItemsForCategory(Category category = null);
    }
}
