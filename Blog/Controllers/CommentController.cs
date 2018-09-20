using Blog.Models;
using Blog.Services;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public ActionResult Create(CommentViewModel commentViewModel)
        {

            var isSaved = _commentService.SaveComment(commentViewModel);

            if (isSaved)
            {
                return RedirectToAction("Detail", "Blog", new { id = commentViewModel.PostId });
            }

            ViewBag.Comment = commentViewModel.Content;
            return View("Detail", "Home");
        }
    }
}
