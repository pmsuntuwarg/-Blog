using Blog.Areas.Admin.Services;
using Blog.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public ViewResult Index(string query, int page = 1, int pageSize = 10)
        {
            if (query is null)
            {
                query = "";
            }

            var posts = _postService.GetAllPosts(query, page, pageSize);
            ViewData["query"] = query;
            ViewData["size"] = pageSize;
            return View(posts);
        }

        public ViewResult Create()
        {
            ViewBag.Tags = _postService.GetSelectListItemsForTags(_tagService.GetAllTags(), null);
            ViewBag.Categories = _categoryService.GetSelectListItemsForCategory();
            ViewData["Action"] = "Create";

            return View("Edit");
        }

        [HttpPost]
        public ActionResult Create(PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                _postService.SavePost(post);
                return RedirectToAction("Index");
            }

            return View("Edit", post);
        }

        public ActionResult Detail(string id)
        {
            var post = _postService.GetPostById(id);

            if (post == null)
            {
                return NotFound("Post not found");
            }

            _postService.IncreaseViewCount(post);

            return View(post);
        }

        [Authorize]
        public ActionResult Delete(string id)
        {
            try
            {
                _postService.DeletePost(id);
                return RedirectToAction("Index");
            } catch
            {
                return RedirectToAction("Detail", new { id });
            }
        }

        [Authorize]
        public ActionResult Edit(string id)
        {
            ViewData["Action"] = "Edit";
            var post = _postService.GetPostById(id);

            PostViewModel postViewModel = new PostViewModel();

            postViewModel.Id = post.PostId;
            postViewModel.Title = post.Title;
            postViewModel.Excerpt = post.Excerpt;
            postViewModel.Content = post.Content;

            ViewBag.Tags = _postService.GetSelectListItemsForTags(_tagService.GetAllTags(), post.PostTags);
            ViewBag.Categories = _categoryService.GetSelectListItemsForCategory(post.Category);

            return View(postViewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                _postService.SavePost(post, "Edit");

                return RedirectToAction("Detail", new { id = post.Id });
            }

            return View(post);
        }
    }
}
