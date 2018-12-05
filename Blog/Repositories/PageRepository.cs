using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Areas.Admin.Models;
using Blog.Data;

namespace Blog.Repositories
{
    public class PageRepository : IPageRepository
    {
        private readonly ApplicationDbContext _context;

        public PageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Page GetPageByName(string pageName)
        {
             return _context.Pages.Where(p => p.PageName == pageName).FirstOrDefault();
        }
    }
}
