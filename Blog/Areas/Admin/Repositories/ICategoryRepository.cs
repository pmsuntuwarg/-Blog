using Blog.Areas.Admin.Models;
using System.Collections.Generic;

namespace Blog.Areas.Admin.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(string categoryId);
        Category GetCategoryByName(string categoryName);
        int SaveCategory(Category category);
        int DeleteCategory(Category category);
        int UpdateCategory(Category category);
    }
}
