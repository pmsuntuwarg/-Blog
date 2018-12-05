using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services
{
    public interface IPageService
    {
        string GetPageContent(string pageName);
    }
}
