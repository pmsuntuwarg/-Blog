using Blog.Entities.Models;
using Blog.Entities.ViewModels;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Interfaces.Admin
{
    public interface IPostService : IBaseService<Post, PostViewModel, string>
    {
        void IncreaseViewCount(PostViewModel post);
        int GetCountByCategory(string categoryId);
        Task<PostViewModel> GetBySlug(string slug);
    }
}
