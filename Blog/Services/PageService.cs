using Blog.Areas.Admin.Models;
using Blog.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;

        public PageService(IPageRepository pageRespository)
        {
            _pageRepository = pageRespository;
        }

        public string GetPageContent(string pageName)
        {
            Page page = _pageRepository.GetPageByName(pageName);
            if(page != null)
            {
                return page.Body;
            }

            return null;
        }
    }
}
