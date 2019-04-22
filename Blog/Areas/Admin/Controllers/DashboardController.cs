using Blog.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class DashboardController : Controller
    {

        public DashboardController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
