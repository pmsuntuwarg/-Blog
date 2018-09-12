using Blog.Models;
using Blog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services
{
    public interface IPostService
    {
        string SavePost(PostViewModel postViewModel, string action = "Create");
        IEnumerable<Post> GetAllPosts(string query, int pageSize, int pageNumber);
        Post GetPostById(string PostId);
        void DeletePost(string PostId);
        void IncreaseViewCount(Post post);
    }
}
