using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models;

namespace Blog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            return _context.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

        public Category GetCategoryById(string categoryId)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
        }

        public Category GetCategoryByName(string categoryName)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryName == categoryName);
        }

        public int SaveCategory(Category category)
        {
            _context.Categories.Add(category);
            return _context.SaveChanges();
        }

        public int UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            return _context.SaveChanges();
        }
    }
}
