using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.Infrastructure.Interfaces.Admin;
using Blog.Entities.ViewModels;
using System.Threading.Tasks;
using Blog.Entities.Models;

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

        public async Task<IActionResult> Index()
        {
            IEnumerable<CategoryViewModel> categories =  await _categoryService.GetAll();

            return View(categories);
        }

        public ActionResult Create()
        {
            ViewBag.Action = "Create";
            return View("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel category)
        {
            try
            {
                await _categoryService.Create(category);

                return RedirectToAction(nameof(Index));
            } catch (Exception e)
            {
            }

            return View("Update", category);
        }

        public async Task<IActionResult> Detail(string categoryId)
        {
            CategoryViewModel category = await _categoryService.GetById(categoryId);
            return View(category);
        }
        
        public async Task<IActionResult> Update(string id)
        {
            CategoryViewModel category = await _categoryService.GetById(id);

            if(category == null)
            {
                ViewBag.Error = "Category Doesn't Exist!!";

                return RedirectToAction(nameof(Index));
            }

            return View("Edit", category);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategoryViewModel category)
        {
            try
            {
                await _categoryService.Create(category);

                return RedirectToAction(nameof(Index));
            } catch(Exception e)
            {
                ModelState.AddModelError("CategoryName", e.Message);
            }

            return View("Edit", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            await _categoryService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
