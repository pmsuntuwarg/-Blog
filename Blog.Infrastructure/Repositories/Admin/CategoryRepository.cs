using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Blog.Data;
using Blog.Data.DbContext;
using Blog.Entities.Models;
using Blog.Infrastructure.Interfaces.Admin;

namespace Blog.Infrastructure.Repositories.Admin
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Category GetCategoryByName(string categoryName)
        {
            return _context.Categories.FirstOrDefault(c => c.Name == categoryName);
        }
    }
}
