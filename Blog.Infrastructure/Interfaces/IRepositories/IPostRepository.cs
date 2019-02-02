using Blog.Entities.Models;
using System.Collections.Generic;

namespace Blog.Infrastructure.Interfaces.Admin
{
    public interface IPostRepository : IBaseRepository
    {
        IEnumerable<Post> RecentPosts(int takeTotal);
       
        void DeletePostTags(IEnumerable<PostTag> postTags);
    }
}
