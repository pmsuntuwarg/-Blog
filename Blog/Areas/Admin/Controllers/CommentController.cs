using Blog.Areas.Admin.Services;
using Blog.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
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
