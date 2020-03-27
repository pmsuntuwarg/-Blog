using Blog.Entities;
using Blog.Entities.Models;
using Blog.Entities.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Interfaces.Admin
{
    public interface IPostService : IBaseService<Post, PostViewModel, string>
    {
        Task IncreaseViewCount(PostViewModel post);
        int GetCountByCategory(string categoryId);
        Task<PostViewModel> GetBySlug(string slug);
        Task<IReadOnlyList<PostViewModel>> GetAllPublished(string searchQuery);
        Task<IReadOnlyList<PostViewModel>> GetAllPopularPosts(int total = 0);
    }
}
