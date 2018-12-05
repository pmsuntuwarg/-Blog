using Blog.Areas.Admin.Models;
using Blog.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Blog.Areas.Admin.Services
{
    public interface IPostService
    {
        string SavePost(PostViewModel postViewModel, string action = "Create");
        IEnumerable<Post> GetAllPosts(string query, int pageSize, int pageNumber);
        Post GetPostById(string postId);
        void DeletePost(string postId);
        void IncreaseViewCount(Post post);
        List<SelectListItem> GetSelectListItemsForTags(IEnumerable<Tag> tags, List<PostTag> selectedTags = null);
        int GetCountByCategory(string categoryId);
    }
}
