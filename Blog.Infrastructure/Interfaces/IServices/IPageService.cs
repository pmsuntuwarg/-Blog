using Blog.Entities.Models;
using Blog.Entities.ViewModels;
using Blog.Entities.ViewModels.DataTable;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Interfaces.Admin
{
    public interface IPageService : IBaseService<Page, PageViewModel, string>
    {
        Task<Page> GetPageByPageName(string pageName);
        Task<List<PageViewModel>> GetPages(DataTableAjaxPostModel model);
    }
}
