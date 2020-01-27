using Blog.Entities.ViewModels;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly IPageService _pageService;
        public HomeController(IPostService postService, IPageService pageService)
        {
            _postService = postService;
            _pageService = pageService;
        }

        public async Task<ViewResult> Index()
        {
            var posts = await _postService.GetAll();

            return View(posts);
        }

        [Route("Post/{slug}")]
        public async Task<IActionResult> Post(string slug)
        {
            PostViewModel post = await _postService.GetBySlug(slug);

            if (post == null)
                return NotFound("Post not found");

            _postService.IncreaseViewCount(post);

            return View(post);
        }

        // [Route("Page/{pageName}")]
        public async Task<IActionResult> Page(string pageName)
        {
            if(pageName != null)
            {
               ViewBag.Content = (await _pageService.GetPageByPageName(pageName)).Body;
            }

            return View();
        }
    }
}
