using Blog.Entities.Models;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Tag> tags = await _tagService.GetAll();

            return View(tags);
        }

        public IActionResult Create()
        {
            return View("Edit", new Tag { });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if(ModelState.IsValid)
            {
                await _tagService.Create(tag);

                return RedirectToAction(nameof(Index));
            }

            return View("Edit", tag);
        }

        public async Task<IActionResult> Update(string id)
        {
            Tag tag = await _tagService.GetById(id);

            if(tag != null)
            {
                return View("Edit", tag);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Tag tag)
        {
            if(ModelState.IsValid)
            {
                await _tagService.Update(tag);

                return RedirectToAction(nameof(Index));
            }

            return View("Edit", tag);
        }

        public async Task<PartialViewResult> Delete(string id)
        {
            Tag tag = await _tagService.GetById(id);

            return PartialView("_DeleteTagModalPartial", tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Tag tag)
        {
            try
            {
                _tagService.Delete(tag.Id);
            } catch
            {
                ViewBag.Error = "Some error occured please try again later.";
            }

            return RedirectToAction(nameof(Index)); ;
        }
    }
}
