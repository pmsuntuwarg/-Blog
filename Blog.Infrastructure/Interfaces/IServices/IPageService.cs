using Blog.Entities.Models;
using Blog.Entities.ViewModels;
using System.Collections.Generic;

namespace Blog.Infrastructure.Interfaces.Admin
{
    public interface IPageService : IBaseService<Page, PageViewModel, string>
    {
        Page GetPageByPageName(string pageName);
    }
}
