using Microsoft.AspNetCore.Builder;

namespace Blog.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMvcWithCustomizedRoute(this IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute("area", "{area:exists}/{controller=Dashboard}/{action=index}/{id?}");
                routes.MapRoute("page", "{controller=Dashboard}/{action=Page}/{pageName}");
                // routes.MapRoute("post", "{controller=Home}/{action=Post}/{slug}");
                routes.MapRoute("default", "{controller=Home}/{action=index}/{id?}");
            });

            return app;
        }
    }
}