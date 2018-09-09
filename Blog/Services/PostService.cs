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
            _postRepository.DeletePost(postId);
        }

        public IEnumerable<Post> GetAllPosts(string query)
        {
            return _postRepository.GetAllPosts(query);
        }

        public Post GetPostById(string postId)
        {
            return _postRepository.GetPostById(postId);
        }

        public string SavePost(PostViewModel postViewModel, string action = "Create")
        {
            try
            {
                Post post = new Post();

                if (action == "Edit")
                    post = _postRepository.GetPostById(postViewModel.Id);

                post.Title = postViewModel.Title;
                post.Excerpt = postViewModel.Excerpt;
                post.Content = postViewModel.Content;

                if (action == "Create")
                    _postRepository.SavePost(post);

                if (action == "Edit")
                    _postRepository.UpdatePost(post);

                return "saved";
            } catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
