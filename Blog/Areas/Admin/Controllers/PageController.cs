using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Blog.Infrastructure.Interfaces.Admin;
using Blog.Entities.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Blog.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class PageController : Controller
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<PageViewModel> pages = await _pageService.GetAll();

            return View(pages);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Action = "Create";

            return View("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Create(PageViewModel pageViewModel)
        {
            try
            {

                await _pageService.Create(pageViewModel);

                return RedirectToAction(nameof(Index));
            } catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return View("Edit", pageViewModel);
        }

        public async Task<IActionResult> Update(string id)
        {
            PageViewModel page = await _pageService.GetById(id);

            if(page == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View("Edit", page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(PageViewModel pageViewModel)
        {
            try
            {
                await _pageService.Update(pageViewModel);

                return RedirectToAction("Detail", new { id = pageViewModel.Id });
            } catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
                return View("Edit", pageViewModel);
        }

        public async Task<IActionResult> Detail(string id)
        {
            PageViewModel page = await _pageService.GetById(id);

            return View(page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(PageViewModel pageViewModel)
        {
            try
            {
                _pageService.Delete(pageViewModel.Id);

                return RedirectToAction(nameof(Index));
            } catch(Exception ex)
            {
                
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
