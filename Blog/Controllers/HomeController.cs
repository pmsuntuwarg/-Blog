using Blog.Services;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        public ViewResult Index(string query)
        {

            if (query is null)
                query = "";
            //query = "soluta";
            var posts = _postService.GetAllPosts(query);
            ViewData["query"] = query;
            return View(posts);
        }

        public ViewResult Create()
        {
            ViewData["Action"] = "Create";

            return View("Edit");
        }

        [HttpPost]
        public ActionResult Create(PostViewModel post)
        {
            if(ModelState.IsValid)
            {
                _postService.SavePost(post);
                return RedirectToAction("Index");
            }

            return View("Edit", post);
        }

        public ViewResult Detail(string id)
        {
            var post = _postService.GetPostById(id);

            return View(post);
        }

        public ActionResult Delete(string id)
        {
            _postService.DeletePost(id);

            return null;
        }

        public ActionResult Edit(string id)
        {
            ViewData["Action"] = "Edit";
            var post = _postService.GetPostById(id);

            PostViewModel postViewModel = new PostViewModel();

            postViewModel.Id = post.PostId;
            postViewModel.Title = post.Title;
            postViewModel.Excerpt = post.Excerpt;
            postViewModel.Content = post.Content;

            return View(postViewModel);
        }

        [HttpPost]
        public ActionResult Edit(PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                _postService.SavePost(post, "Edit");

                return RedirectToAction("Detail", new { id = post.Id } );
            }

            return View(post);
        }
    }
}
