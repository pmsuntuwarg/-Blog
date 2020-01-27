using Blog.Entities.Models;
using Blog.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Interfaces.Admin
{
    public interface IPageRepository : IBaseRepository
    {
        Task<Page> GetPageByPageName(string pageName);
        List<Page> GetPages(Expression<Func<Page, bool>> whereClause, string searchBy, int take, int skip, string sortBy, bool sortDir);
    }
}
