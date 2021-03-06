﻿using Blog.Common.Helpers;
using Blog.Entities;
using Blog.Entities.Models;
using Blog.Entities.ViewModels;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services.Admin
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<DataResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {

            }

            Category category = await _categoryRepository.GetById<Category, string>(id);

            return await _categoryRepository.Delete(category);
        }

        public async Task<IReadOnlyList<CategoryViewModel>> GetAll(string searchQuery)
        {
            return await (from result in _categoryRepository.GetAllAsync<Category>()
                          select new CategoryViewModel
                          {
                              Id = result.Id,
                              CategoryName = result.Name
                          }).ToListAsync();
        }

        public async Task<CategoryViewModel> GetById(string categoryId)
        {
            Category model = await _categoryRepository.GetById<Category, string>(categoryId);

            return new CategoryViewModel
            {
                CategoryName = model.Name
            };

        }

        public Task<DataResult> Create(CategoryViewModel viewModel)
        {
            Category oldCategory = _categoryRepository.GetCategoryByName(viewModel.CategoryName);

            if (oldCategory != null)
            {
                throw new Exception("Category Already Exist");
            }

            Category category = new Category
            {
                Id = viewModel.Id,
                Name = viewModel.CategoryName,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            return _categoryRepository.Create(category);
        }

        public Task<DataResult> Update(CategoryViewModel viewModel)
        {
            Category oldCategory = _categoryRepository.GetCategoryByName(viewModel.CategoryName);

            if (oldCategory != null)
            {
                throw new Exception("Category Already Exist");
            }

            Category category = new Category
            {
                Id = viewModel.Id,
                Name = viewModel.CategoryName,
                UpdatedDate = DateTime.Now,
            };

            return _categoryRepository.Update(category);
        }

        public Task<PaginatedList<CategoryViewModel>> GetPaginatedList(int? page, int? pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
