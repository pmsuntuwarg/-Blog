using Blog.Entities.Models;

namespace Blog.Infrastructure.Interfaces.Admin
{
    public interface ICategoryRepository : IBaseRepository
    {
        Category GetCategoryByName(string categoryName);
    }
}
