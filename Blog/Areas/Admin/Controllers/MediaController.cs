using Blog.Entities;
using Blog.Entities.ViewModels;
using Blog.Entities.ViewModels.DataTable;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MediaController : Controller
    {
        public readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View("Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]MediaViewModel mediaViewModel)
        {
            if (ModelState.IsValid)
            {
                DataResult result = await _mediaService.Create(mediaViewModel);

                return RedirectToAction(nameof(Detail), new { id = result.ReturnId });
            }

            return View("Edit", mediaViewModel);
        }

        public async Task<IActionResult> Detail(string id)
        {
            MediaViewModel media = await _mediaService.GetById(id);

            return View("Detail", media);
        }

        [HttpPost]
        [ResponseCache(Duration = 200)]
        public async Task<IActionResult> GetMedias(DataTableAjaxModel model)
        {
            var result = await _mediaService.GetMedias(model);

            return Json(new
            {
                //thigs to send to datatable - mandatory
                draw = model.draw,
                recordsTotal = 0,//result.Count != 0 ? result[0].TotalCount : 0,      // totalResultsCount,
                recordsFiltered = 0,//result.Count != 0 ? result[0].FilteredCount : 0,   // filteredResultsCount,
                data = result
            });
        }
    }
}