using Blog.Areas.Admin.Services;
using Blog.Areas.Admin.ViewModels;
using BServices = Blog.Services;    
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly BServices.IPageService _pageService;
        public HomeController(IPostService postService, BServices.IPageService pageService)
        {
            _postService = postService;
            _pageService = pageService;
        }

        public ViewResult Index(string query, int page = 1, int pageSize = 10)
        {
            if (query is null)
                query = "";
                
            var posts = _postService.GetAllPosts(query, page, pageSize);
            ViewData["query"] = query;
            ViewData["size"] = pageSize;
            return View(posts);
        }

        public ActionResult Detail(string id)
        {
            var post = _postService.GetPostById(id);

            if (post == null)
                return NotFound("Post not found");
            _postService.IncreaseViewCount(post);

            return View(post);
        }
        

        public ActionResult Page(string pageName)
        {
            if(pageName != null)
            {
                ViewBag.Content = _pageService.GetPageContent(pageName);
            }

            return View();
        }
    }
}
