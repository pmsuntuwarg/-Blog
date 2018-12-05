using System;
using System.Collections.Generic;
using Blog.Areas.Admin.Models;
using Blog.Areas.Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            IEnumerable<Category> categories =  _categoryService.GetAllCategories();

            return View(categories);
        }

        public ActionResult Create()
        {
            ViewBag.Action = "Create";
            return View("Edit");
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                _categoryService.SaveCategory(category);

                return RedirectToAction(nameof(Index));
            } catch (Exception e)
            {
            }

            return View("Edit", category);
        }

        public ActionResult Detail(string categoryId)
        {
            Category  category = _categoryService.GetCategoryById(categoryId);
            return View(category);
        }
        
        public ActionResult Edit(string id)
        {
            ViewBag.Action = "Edit";
            Category category = _categoryService.GetCategoryById(id);

            if(category == null)
            {
                ViewBag.Error = "Category Doesn't Exist!!";

                return RedirectToAction(nameof(Index));
            }

            return View("Edit", category);
        }
        
        [HttpPost, ActionName("Edit")]
        public ActionResult Update(Category category)
        {
            try
            {
                _categoryService.SaveCategory(category, "Edit");

                return RedirectToAction(nameof(Index));
            } catch(Exception e)
            {
                ModelState.AddModelError("CategoryName", e.Message);
            }

            return View("Edit", category);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            _categoryService.DeleteCategory(category);

            return RedirectToAction(nameof(Index));
        }
    }
}
