using Blog.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.ViewComponents
{
    public class CategoryPostCountViewComponent : ViewComponent
    {
        private readonly IPostService _postService;
        public CategoryPostCountViewComponent(IPostService postService)
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
