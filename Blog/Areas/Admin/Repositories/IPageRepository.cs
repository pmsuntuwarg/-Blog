using Blog.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.Repositories
{
    public interface IPageRepository
    {
        int Save(Page page);
        int Update(Page page);
        int Delete(Page page);

        Page GetPageById(string pageId);
        Page GetPageByPageName(string pageName);
        IEnumerable<Page> GetAllPages();
    }
}
