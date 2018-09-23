using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repositories
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
