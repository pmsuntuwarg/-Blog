using Blog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repositories
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAllPosts(string query, int pageNumber, int pageSize);
        Post GetPostById(string postId);
        void SavePost(Post post);
        void UpdatePost(Post post);
        void DeletePost(Post post);
        IEnumerable<Tag> GetAllTags();
    }
}
