using Blog.Entities.Models.Identity;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }
    }
}
