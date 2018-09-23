using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public int DeleteCategory(Category category)
        {
            return _categoryRepository.DeleteCategory(category);

        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }

        public Category GetCategoryById(string categoryId)
        {
            return _categoryRepository.GetCategoryById(categoryId);
        }

        public List<SelectListItem> GetSelectListItemsForCategory(Category selectedCategory = null)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var categories = _categoryRepository.GetAllCategories();

            foreach (Category category in categories)
            {
                if(selectedCategory != null)
                {
                    items.Add(new SelectListItem {
                        Text = category.CategoryName,
                        Value = category.CategoryId,
                        Selected = category.CategoryId == selectedCategory.CategoryId
                    });
                    continue;
                }

                items.Add(new SelectListItem {
                    Text = category.CategoryName,
                    Value = category.CategoryId
                });
            }

            return items;
        }

        public int SaveCategory(Category category, string action = "Create")
        {
            Category oldCategory = _categoryRepository.GetCategoryByName(category.CategoryName);

            if (oldCategory != null)
            {
                throw new Exception("Category Already Exist");
            }

            if (action == "Edit")
            {
                return _categoryRepository.UpdateCategory(category);
            }

            return _categoryRepository.SaveCategory(category);
        }
    }
}
