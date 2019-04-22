using Blog.Common.Helpers;
using Blog.Entities.Models;
using Blog.Entities.ViewModels;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        public BlogController(IPostService postService, ICategoryService categoryService, ITagService tagService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var posts = await _postService.GetPaginatedList(page, null);

            return View(posts);
        }

        public async Task<IActionResult> Create()
        {
            PostViewModel model = new PostViewModel
            {
                CategoryId = string.Empty,
                Tags = await _tagService.GetAll(),
                Categories = await _categoryService.GetAll()
            };

            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                await _postService.Create(post);

                return RedirectToAction("Index");
            }

            post.Tags = await _tagService.GetAll();
            post.Categories = await _categoryService.GetAll();

            return View("Edit", post);
        }

        public async Task<IActionResult> Detail(string id)
        {
            PostViewModel post = await _postService.GetById(id);

            if (post == null)
            {
                return NotFound("Post not found");
            }

            _postService.IncreaseViewCount(post);

            return View(post);
        }

        public ActionResult Delete(string id)
        {
            try
            {
                _postService.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Detail", new { id });
            }
        }

        public async Task<ActionResult> Update(string id)
        {
            PostViewModel post = await _postService.GetById(id);
            post.Categories = await _categoryService.GetAll();
            post.Tags = await _tagService.GetAll();

            foreach(PostTag postTag in post.PostTags)
            {
                post.TagIds.Add(postTag.TagId);
            }

            return View("Edit", post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm]PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                await _postService.Update(post);

                return RedirectToAction("Detail", new { id = post.Id });
            }

            post.Categories = await _categoryService.GetAll();
            post.Tags =await  _tagService.GetAll();

            return View("Edit", post);
        }
    }
}
