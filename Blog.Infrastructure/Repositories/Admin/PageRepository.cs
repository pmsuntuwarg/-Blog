using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Data.DbContext;
using Blog.Entities.Models;
using Blog.Entities.ViewModels;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories.Admin
{
    public class PageRepository : BaseRepository, IPageRepository
    {
        private readonly ApplicationDbContext _context;

        public PageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Page GetPageByPageName(string pageName)
        {
            return _context.Pages.FirstOrDefault(p => p.PageName == pageName);
        }

        public List<Page> GetPages(Expression<Func<Page, bool>> whereClause, string searchBy, int take, int skip, string sortBy, bool sortDir)
        {
            if (String.IsNullOrEmpty(searchBy))
            {
                // if we have an empty search then just order the results by Id ascending
                sortBy = "Id";
                sortDir = true;
            }

            var result = _context.Pages
                           //.AsExpandable()
                           .Where(whereClause)
                           .Select(m => new Page
                           {
                               Id = m.Id,
                               PageName = m.PageName,
                               Title = m.Title,
                               IsPublished = m.IsPublished,
                               CreatedDate = m.CreatedDate,
                               UpdatedDate = m.UpdatedDate,
                               IsDraft = m.IsDraft
                           })
                           //.OrderBy(sortBy, sortDir) // have to give a default order when skipping .. so use the PK
                           .Skip(skip)
                           .Take(take)
                           .ToList();

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            result.Select(m=>m.TotalCount =  _context.Pages.Count()).FirstOrDefault();

            return result;
        }
    }
}
