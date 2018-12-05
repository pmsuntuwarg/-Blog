using Blog.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Blog.Areas.Admin.Services
{
    public interface ICategoryService
    {
        int SaveCategory(Category category, string action = "Create");
        int DeleteCategory(Category category);

        Category GetCategoryById(string categoryId);
        IEnumerable<Category> GetAllCategories();
        List<SelectListItem> GetSelectListItemsForCategory(Category category = null);
    }
}
