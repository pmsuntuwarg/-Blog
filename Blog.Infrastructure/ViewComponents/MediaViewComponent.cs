using Blog.Entities.Models.Identity;
using Blog.Entities.ViewModels;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Infrastructure.ViewComponents
{
    public class MediaViewComponent : ViewComponent
    {
        private readonly IMediaService _mediaService;
        public MediaViewComponent(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var posts = await _mediaService.GetPaginatedList(1, null);

            return View(posts);
        }
    }
}
