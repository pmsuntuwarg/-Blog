using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        Post GetPostById(string postId);
        void DeletePost(string postId);
        void IncreaseViewCount(Post post);
        IEnumerable<Tag> GetAllTags();
        List<SelectListItem> GetSelectListItemsForTags(List<PostTag> selectedTags = null);
    }
}
