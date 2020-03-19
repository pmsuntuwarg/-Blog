using Blog.Infrastructure.Interfaces.Admin;
using Blog.Infrastructure.Interfaces.IRepositories;
using Blog.Infrastructure.Repositories.Admin;
using Blog.Infrastructure.Services.Admin;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure.Extensions
{
    public static class DependencyResolverExtension
    {
        public static void DependencyResolver(this IServiceCollection services)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IMediaRepository, MediaRepository>();

            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IMediaService, MediaService>();
        }
    }
}