using Blog.Areas.Admin.Models;
using Blog.Areas.Admin.ViewModels;
using System.Collections.Generic;

namespace Blog.Areas.Admin.Services
{
    public interface IPageService
    {
        int SavePage(PageViewModel pageViewModel, string action = "Create");
        void DeletePage(string pageId);

        Page GetPageById(string pageId);
        Page GetPageByPageName(string pageName);
        IEnumerable<Page> GetAllPages();
    }
}
