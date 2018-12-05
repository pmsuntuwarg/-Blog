using Blog.Areas.Admin.Models;
using Blog.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Blog.Areas.Admin.Controllers
{
    [Area("admin")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IActionResult Index()
        {
            var tags = _tagService.GetAllTags();

            return View(tags);
        }

        public IActionResult Create()
        {
            ViewBag.Action = "Create";
            return View("Edit");
        }

        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            if(ModelState.IsValid)
            {
                _tagService.SaveTag(tag);

                return RedirectToAction(nameof(Index));
            }

            return View("Edit", tag);
        }

        public IActionResult Edit(string id)
        {
            var tag = _tagService.GetTagById(id);

            if(tag != null)
            {
                ViewBag.Action = "Edit";
                return View("Edit", tag);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(Tag tag)
        {
            if(ModelState.IsValid)
            {
                _tagService.SaveTag(tag, "Update");

                return RedirectToAction(nameof(Index));
            }

            return View("Edit", tag);
        }

        public PartialViewResult Delete(string id)
        {
            Tag tag = _tagService.GetTagById(id);

            return PartialView("_DeleteTagModalPartial", tag);
        }

        [HttpPost]
        public IActionResult Delete(Tag tag)
        {
            try
            {
                _tagService.DeleteTag(tag);
            } catch
            {
                ViewBag.Error = "Some error occured please try again later.";
            }

            return RedirectToAction(nameof(Index)); ;
        }
    }
}
