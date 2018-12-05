using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Areas.Admin.Models;
using Blog.Areas.Admin.Repositories;
using Blog.Areas.Admin.ViewModels;

namespace Blog.Areas.Admin.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRespository;

        public PageService(IPageRepository pageRepository)
        {
            _pageRespository = pageRepository;
        }

        public void DeletePage(string pageId)
        {
            Page page = _pageRespository.GetPageById(pageId);

            if(page != null)
            {
                _pageRespository.Delete(page);
            }
        }

        public IEnumerable<Page> GetAllPages()
        {
            return _pageRespository.GetAllPages();
        }

        public Page GetPageById(string pageId)
        {
            Page page = _pageRespository.GetPageById(pageId);

            if(page != null)
            {
                return page;
            }

            throw new Exception("Not found");
        }

        public Page GetPageByPageName(string pageName)
        {
            return _pageRespository.GetPageByPageName(pageName);
        }

        public int SavePage(PageViewModel pageViewModel, string action = "Create")
        {
            Page page = new Page {
                Title = pageViewModel.Title,
                PageName = pageViewModel.PageName,
                Body = pageViewModel.Body,
                PageSlug = CreateSlug(pageViewModel.PageName)
            };

            if (action == "Create")
            {
                page.CreatedAt = DateTime.UtcNow;
                page.isPublished = false;
                page.isDraft = true;
            }
            if (action == "Edit")
            {
                page.UpdatedAt = DateTime.UtcNow;
            }

            _pageRespository.Save(page);

            return 1;
        }

        private string CreateSlug(string pageName)
        {
            var slug = pageName.ToLowerInvariant().Replace(" ", "-");

            return slug;
        }
    }
}
