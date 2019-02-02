using Blog.Entities.Models;
using Blog.Entities.ViewModels;
using System.Collections.Generic;

namespace Blog.Infrastructure.Interfaces.Admin
{
    public interface ICategoryService : IBaseService<Category, CategoryViewModel, string>
    {

    }
}
