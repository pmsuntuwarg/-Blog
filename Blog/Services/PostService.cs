using Blog.Models;
using Blog.Repositories;
using Blog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public void DeletePost(string postId)
        {
            Post post = GetPostById(postId);

            if (post != null)
                _postRepository.DeletePost(post);
        }

        public IEnumerable<Post> GetAllPosts(string query, int pageNumber, int pageSize)
        {
            var posts = _postRepository.GetAllPosts(query,pageNumber, pageSize);

            return posts;
        }

        public Post GetPostById(string postId)
        {
            return _postRepository.GetPostById(postId);
        }

        public void IncreaseViewCount(Post post)
        {
            if (post.IsPublished)
                post.ViewCount += 1;
        }

        public string SavePost(PostViewModel postViewModel, string action = "Create")
        {
            try
            {
                Post post = new Post();

                if (action == "Edit")
                {
                    post = _postRepository.GetPostById(postViewModel.Id);
                    post.CreatedAt = post.CreatedAt;
                }

                if (post.Title != postViewModel.Title)
                    post.Slug = CreateSlug(postViewModel.Title);

                post.Title = postViewModel.Title;
                post.Excerpt = postViewModel.Excerpt;
                post.Content = postViewModel.Content;

                if (action == "Create")
                {
                    _postRepository.SavePost(post);
                }

                if (action == "Edit")
                    _postRepository.UpdatePost(post);

                return "saved";
            } catch (Exception e)
            {
                return e.Message;
            }
        }

        private string CreateSlug(string title)
        {
            var slug = title.ToLowerInvariant().Replace(" ", "-");
            return slug;
        }
    }
}
