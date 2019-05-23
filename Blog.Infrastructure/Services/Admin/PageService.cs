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
using Blog.Entities.ViewModels.DataTable;
using System.Linq.Expressions;

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

        public async Task<List<PageViewModel>> GetPages(DataTableAjaxPostModel model)
        {

            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;

            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }

            if (String.IsNullOrEmpty(searchBy))
            {
                // if we have an empty search then just order the results by Id ascending
                sortBy = "Id";
                sortDir = true;
            }

            var whereClause = BuildDynamicWhereClause(searchBy);
            // search the dbase taking into consideration table sorting and paging
            List<PageViewModel> result = (from a in _pageRespository.GetPages(whereClause, searchBy, take, skip, sortBy, sortDir)
            select new PageViewModel
            {
                Id = a.Id,
                Body = a.Body,
                PageName = a.PageName,
                Title = a.Title
            }).ToList();

            if (result == null)
            {
                // empty collection...
                return new List<PageViewModel>();
            }

            return result;
        }
        private Expression<Func<Page, bool>> BuildDynamicWhereClause(string searchValue)
        {
            // simple method to dynamically plugin a where clause
            var predicate = PredicateBuilder.True<Page>();// true -where(true) return all
            if (String.IsNullOrWhiteSpace(searchValue) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchValue.Split(' ').ToList().ConvertAll(x => x.ToLower());

                predicate = predicate.Or(s => searchTerms.Any(srch => s.PageName.ToLower().Contains(srch)));
                predicate = predicate.Or(s => searchTerms.Any(srch => s.Title.ToLower().Contains(srch)));
            }

            return predicate;
        }
    }
}
