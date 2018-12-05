using Blog.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repositories
{
    public interface IPageRepository
    {
        Page GetPageByName(string pageName); 
    }
}
