using Blog.Data.DbContext;
using Blog.Entities.Models;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories.Admin
{
    public class PageRepository : BaseRepository, IPageRepository
    {
        private readonly ApplicationDbContext _context;

        public PageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Page> GetPageByPageName(string pageName)
        {
            return await _context.Pages.FirstOrDefaultAsync(p => p.PageName == pageName);
        }

        public List<Page> GetPages(Expression<Func<Page, bool>> whereClause, string searchBy, int take, int skip, string sortBy, bool sortDir)
        {
            if (string.IsNullOrEmpty(searchBy))
            {
                // if we have an empty search then just order the results by Id ascending
                sortBy = "Id";
                sortDir = true;
            }

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            int totalCount = _context.Pages.Count();//(m=>m.TotalCount =  _context.Pages.Count());
            int filteredCount = _context.Pages.Count(whereClause);//(m=>m.TotalCount =  _context.Pages.Count());

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
                               IsDraft = m.IsDraft,
                               TotalCount = totalCount,
                               FilteredCount = filteredCount
                           })
                           //.OrderBy(sortBy, sortDir) // have to give a default order when skipping .. so use the PK
                           .Skip(skip)
                           .Take(take)
                           .ToList();

            return result;
        }
    }
}
