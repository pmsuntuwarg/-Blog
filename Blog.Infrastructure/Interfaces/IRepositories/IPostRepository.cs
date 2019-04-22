using Blog.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Interfaces.Admin
{
    public interface IPostRepository : IBaseRepository
    {
        IEnumerable<Post> RecentPosts(int takeTotal);
        Task<Post> GetById(string postId);
    }
}
