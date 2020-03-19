using Blog.Common.Enums;
using Blog.Entities;
using Blog.Entities.ViewModels;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommentViewModel commentViewModel)
        {

            DataResult result = await _commentService.Create(commentViewModel);

            if (result.Status == Status.Success)
            {
                return RedirectToAction("Detail", "Blog", new { id = commentViewModel.PostId });
            }

            ViewBag.Comment = commentViewModel.Content;
            return View("Detail", "Home");
        }
    }
}
