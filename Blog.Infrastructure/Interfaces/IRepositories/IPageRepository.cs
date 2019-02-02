using Blog.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Interfaces.Admin
{
    public interface IPageRepository : IBaseRepository
    {
        Page GetPageByPageName(string pageName);
    }
}
