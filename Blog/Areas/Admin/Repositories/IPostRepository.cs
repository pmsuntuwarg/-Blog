using Blog.Areas.Admin.Models;
using System.Collections.Generic;

namespace Blog.Areas.Admin.Repositories
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAllPosts(string query, int pageNumber, int pageSize);
        IEnumerable<Post> RecentPosts(int takeTotal);
        Post GetPostById(string postId);
        void SavePost(Post post);
        void UpdatePost(Post post);
        void DeletePost(Post post);
        void DeletePostTags(IEnumerable<PostTag> postTags);
    }
}
