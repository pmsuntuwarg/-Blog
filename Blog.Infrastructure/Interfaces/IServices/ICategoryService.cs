using Blog.Entities.Models;
using Blog.Entities.ViewModels;

namespace Blog.Infrastructure.Interfaces.Admin
{
    public interface ICategoryService : IBaseService<Category, CategoryViewModel, string>
    {

    }
}
