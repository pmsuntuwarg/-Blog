using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Data.DbContext;
using Blog.Entities.Models;
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
    }
}
