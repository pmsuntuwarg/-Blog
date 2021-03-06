﻿using Blog.Common.Helpers;
using Blog.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Interfaces
{
    public interface IBaseService<TModel, TViewModel, TKey> where TModel : class where TViewModel : class
    {
        Task<DataResult> Create(TViewModel viewModel);
        Task<DataResult> Update(TViewModel viewModel);
        Task<DataResult> Delete(string id);
        Task<IReadOnlyList<TViewModel>> GetAll(string searchQuery = "");
        Task<TViewModel> GetById(TKey key);
        Task<PaginatedList<TViewModel>> GetPaginatedList(int? page, int? pageSize);
    }
}
