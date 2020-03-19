using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Infrastructure.ViewComponents
{
    public class NotificationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
