using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MediaController : Controller
    {
        public readonly IMediaService _mediaService;

        public MediaController()
        {

        }


    }
}