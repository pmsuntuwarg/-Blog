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
            return null;

            //if(isSaved)
            //{
            //    return RedirectToAction("Detail/"+commentViewModel.PostId, "Home");
            //}

            //return RedirectToRoute("Detail", "Home", (string) commentViewModel.PostId, commentViewModel);
            //return RedirectToRoute("Detail", "Home", (string) commentViewModel.PostId);
        }
    }
}
