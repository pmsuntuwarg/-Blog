using Blog.Entities.ViewModels;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        public ViewResult Index()
        {
            var posts = _postService.GetAll();

            return View(posts);
        }

        public async Task<IActionResult> Detail(string id)
        {
            PostViewModel post = await _postService.GetById(id);

            if (post == null)
                return NotFound("Post not found");

            _postService.IncreaseViewCount(post);

            return View(post);
        }
        

        public async Task<IActionResult> Page(string pageName)
        {
            //if(pageName != null)
            //{
            //    ViewBag.Content = await _pastService.GetPageByName(pageName);
            //}

            return View();
        }
    }
}
