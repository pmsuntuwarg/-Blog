using Blog.Areas.Admin.Models;
using Blog.Areas.Admin.Repositories;
using Blog.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Blog.Areas.Admin.Services
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
            {
                _postRepository.DeletePost(post);
            }
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

        // needs to refactor
        public List<SelectListItem> GetSelectListItemsForTags(IEnumerable<Tag> tags, List<PostTag> selectedTags = null)
        {
            List<SelectListItem> item = new List<SelectListItem>();

            foreach (var tag in tags)
            {
                if (selectedTags is null)
                    item.Add(new SelectListItem { Text = tag.TagName, Value = tag.TagId });

                if (selectedTags != null)
                {
                    SelectListItem newItem = null;
                    foreach (var selectedTag in selectedTags)
                    {
                        newItem = new SelectListItem {
                            Text = tag.TagName,
                            Value = tag.TagId,
                            Selected = tag.TagId == selectedTag.TagId
                        };
                        if (tag.TagId == selectedTag.TagId)
                            break ;
                    }

                    if(newItem !=null)
                        item.Add(newItem);
                }
            }

            return item;
        }

        public void IncreaseViewCount(Post post)
        {
            if (post.IsPublished)
            {
                post.ViewCount += 1;
            }
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
                {
                    post.Slug = CreateSlug(postViewModel.Title);
                }

                post.Title = postViewModel.Title;
                post.Excerpt = postViewModel.Excerpt;
                post.Content = postViewModel.Content;
                post.CategoryId = postViewModel.Category;
                RemoveAllTagsByPostId(post.PostId);

                foreach (var tagId in postViewModel.PostTags)
                {
                    post.PostTags.Add(new PostTag {
                        PostId = post.PostId,
                        TagId = tagId
                    });
                }

                //post.PostTags = postViewModel.PostTags.ToList();

                if (action == "Create")
                {
                    _postRepository.SavePost(post);
                }

                if (action == "Edit")
                {
                    post.UpdatedAt = DateTime.UtcNow;
                    _postRepository.UpdatePost(post);
                }

                return "saved";
            } catch (Exception e)
            {
                return e.Message;
            }
        }

        private void RemoveAllTagsByPostId(string postId)
        {
            Post post = _postRepository.GetPostById(postId);
            _postRepository.DeletePostTags(post.PostTags);
        }

        private string CreateSlug(string title)
        {
            var slug = title.ToLowerInvariant().Replace(" ", "-");
            return slug;
        }

        public int GetCountByCategory(string categoryName)
        {
            throw new NotImplementedException();
            // return _postRepository.GetCountByCategory(categoryName);
        }
    }
}
