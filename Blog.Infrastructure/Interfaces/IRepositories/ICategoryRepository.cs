using Blog.Entities.Models;
using Blog.Infrastructure.Repositories;
using System.Collections.Generic;

namespace Blog.Infrastructure.Interfaces.Admin
{
    public interface ICategoryRepository : IBaseRepository
    {
        Category GetCategoryByName(string categoryName);
    }
}
