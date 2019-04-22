using Blog.Common.Enums;
using Blog.Entities.Models;
using Blog.Entities.ViewModels;
using Blog.Entities;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Common.Helpers;

namespace Blog.Infrastructure.Services.Admin
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRespository;

        public PageService(IPageRepository pageRepository)
        {
            _pageRespository = pageRepository;
        }

        public async Task<DataResult> Delete(string id)
        {
            Page page = await _pageRespository.GetById<Page, string>(id);

            if (page != null)
            {
                return await _pageRespository.Delete(page);
            }

            return new DataResult
            {
                Status = Status.Failed,
                Message = "Cannot delete"
            };

        }

        public async Task<IReadOnlyList<PageViewModel>> GetAll()
        {
            return await (from result in _pageRespository.GetAllAsync<Page>()
                          select new PageViewModel
                          {
                              Id = result.Id,
                              Title = result.Title,
                              Body = result.Body,
                              PageName = result.PageName
                          }).ToListAsync();
        }

        public async Task<PageViewModel> GetById(string pageId)
        {
            Page result = await _pageRespository.GetById<Page, string>(pageId);

            if (result != null)
            {
                return new PageViewModel
                {
                    Id = result.Id,
                    PageName = result.PageName,
                    Title = result.Title,
                    Body = result.Body
                };
            }

            throw new Exception("Not found");
        }

        public Page GetPageByPageName(string pageName)
        {
            return _pageRespository.GetPageByPageName(pageName);
        }

        public async Task<DataResult> Create(PageViewModel viewModel)
        {
            Page page = new Page
            {
                Title = viewModel.Title,
                PageName = viewModel.PageName,
                Body = viewModel.Body,
                PageSlug = CreateSlug(viewModel.PageName),
                CreatedDate = DateTime.UtcNow,
                IsPublished = false,
                IsDraft = true
            };

            return await _pageRespository.Create(page);
        }

        public async Task<DataResult> Update(PageViewModel viewModel)
        {
            Page page = new Page
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                PageName = viewModel.PageName,
                Body = viewModel.Body,
                PageSlug = CreateSlug(viewModel.PageName),
                UpdatedDate = DateTime.UtcNow
            };

            return await _pageRespository.Update(page);
        }

        private string CreateSlug(string pageName)
        {
            var slug = pageName.ToLowerInvariant().Replace(" ", "-");

            return slug;
        }

        public Task<PaginatedList<PageViewModel>> GetPaginatedList(int? page, int? pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
