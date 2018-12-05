using System;
using Blog.Areas.Admin.Models;
using Blog.Areas.Admin.Services;
using Blog.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

        public IActionResult Index()
        {
            var pages = _pageService.GetAllPages();

            return View(pages);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Action = "Create";

            return View("Edit");
        }

        [HttpPost]
        public IActionResult Create(PageViewModel pageViewModel)
        {
            try
            {
                Page page = _pageService.GetPageByPageName(pageViewModel.PageName);

                if(page !=null)
                {
                    throw new Exception("Page Aready Exist");
                }

                _pageService.SavePage(pageViewModel);

                return RedirectToAction(nameof(Index));
            } catch(Exception e)
            {
                ModelState.AddModelError("ThrowError", e.Message);
            }

            return View("Edit", pageViewModel);
        }

        public IActionResult Edit(string id)
        {
            ViewBag.Action = "Edit";
            Page page = _pageService.GetPageById(id);

            if(page == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View("Edit",
                new PageViewModel {
                    PageId = page.PageId,
                    Body = page.Body,
                    Title = page.Title,
                    PageName = page.PageName
                });
        }

        [HttpPost]
        public IActionResult Edit(PageViewModel pageViewModel)
        {
            try
            {
                _pageService.SavePage(pageViewModel, "Edit");
                Page page = _pageService.GetPageById(pageViewModel.PageId);

                return RedirectToAction("Detail", new { id = page.PageId });
            } catch (Exception e)
            {
                return View("Edit", pageViewModel);
            }
        }

        public IActionResult Detail(string id)
        {
            Page page = _pageService.GetPageById(id);

            return View(page);
        }

        [HttpPost]
        public IActionResult Delete(PageViewModel pageViewModel)
        {
            try
            {
                _pageService.DeletePage(pageViewModel.PageId);

                return RedirectToAction(nameof(Index));
            } catch(Exception e)
            {
                
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
