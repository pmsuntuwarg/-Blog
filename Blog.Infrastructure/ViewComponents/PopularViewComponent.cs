using Blog.Common.Enums;
using Blog.Entities.ViewModels;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.ViewComponents
{
    public class PopularViewComponent : ViewComponent
    {
        private readonly IPostService _service;
        public PopularViewComponent(IPostService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync(Popular popular)
        {
            if (popular == Popular.Post)
            {
                IReadOnlyList<PostViewModel> posts = await _service.GetAllPopularPosts(total: 4);

                return View("PopularPost", posts);
            }
            return View();
        }
    }
}
