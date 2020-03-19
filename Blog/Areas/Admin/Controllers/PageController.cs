using Blog.Entities.ViewModels;
using Blog.Entities.ViewModels.DataTable;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
            //IEnumerable<PageViewModel> pages = await _pageService.GetAll();

            //return View(pages);

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Action = "Create";

            return View("Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PageViewModel pageViewModel)
        {
            try
            {

                await _pageService.Create(pageViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return View("Edit", pageViewModel);
        }

        public async Task<IActionResult> Update(string id)
        {
            PageViewModel page = await _pageService.GetById(id);

            if (page == null)
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
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
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
        public async Task<IActionResult> Delete(PageViewModel pageViewModel)
        {
            await _pageService.Delete(pageViewModel.Id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ResponseCache(Duration = 200)]
        public async Task<IActionResult> GetPages(DataTableAjaxModel model)
        {
            var result = await _pageService.GetPages(model);

            return Json(new
            {
                //thigs to send to datatable - mandatory
                draw = model.draw,
                recordsTotal = result.Count != 0 ? result[0].TotalCount : 0,      // totalResultsCount,
                recordsFiltered = result.Count != 0 ? result[0].FilteredCount : 0,   // filteredResultsCount,
                data = result
            });
        }
    }
}
