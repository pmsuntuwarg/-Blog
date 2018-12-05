using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Areas.Admin.Models;
using Blog.Data;

namespace Blog.Areas.Admin.Repositories
{
    public class PageRepository : IPageRepository
    {
        private readonly ApplicationDbContext _context;

        public PageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Delete(Page page)
        {
            _context.Remove(page);
            return _context.SaveChanges();
        }

        public IEnumerable<Page> GetAllPages()
        {
            return _context.Pages;
        }
        
        public Page GetPageById(string pageId)
        {
            return _context.Pages.FirstOrDefault(p => p.PageId == pageId);
        }

        public Page GetPageByPageName(string pageName)
        {
            return _context.Pages.FirstOrDefault(p => p.PageName == pageName);
        }

        public int Save(Page page)
        {
            _context.Add(page);

            return _context.SaveChanges();
        }

        public int Update(Page page)
        {
            _context.Update(page);
            return _context.SaveChanges();
        }
    }
}
