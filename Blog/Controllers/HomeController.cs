﻿using Blog.Services;
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

        public ViewResult Index(string query, int page = 1, int pageSize = 10)
        {
            if (query is null)
                query = "";
                
            var posts = _postService.GetAllPosts(query, page, pageSize);
            ViewData["query"] = query;
            ViewData["size"] = pageSize;
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

        public ActionResult Detail(string id)
        {
            var post = _postService.GetPostById(id);

            if (post == null)
                return NotFound("Post not found");
            _postService.IncreaseViewCount(post);

            return View(post);
        }

        public ActionResult Delete(string id)
        {
            try
            {
                _postService.DeletePost(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Detail", new { id });
            }
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
