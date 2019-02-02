using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.ViewComponents
{
    public class PostCountViewComponent : ViewComponent
    {
        private readonly IPostService _postService;
        public PostCountViewComponent(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            throw new Exception("Not implemented");

            return View();
        }
    }
}
