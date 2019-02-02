using Blog.Common.Enums;
using Blog.Entities.Models;
using Blog.Entities.ViewModels;
using Blog.Entities;
using Blog.Infrastructure.Interfaces;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services.Admin
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<DataResult> Delete(string id)
        {
            Post post = await _postRepository.GetById<Post, string>(id);

            if (post != null)
            {
                return await _postRepository.Delete(post);
            }

            return new DataResult
            {
                Status = Status.Failed,
                Message = "Cannot delete"
            };
        }

        public async Task<IEnumerable<PostViewModel>> GetAll()
        {
            return await (from result in _postRepository.GetAllAsync<Post>()
                          select new PostViewModel
                          {
                              Id = result.Id,
                              Title = result.Content,
                              Excerpt = result.Excerpt,
                              Content = result.Content,
                              CategoryId = result.CategoryId,
                              PostTags = result.PostTags
                          }).ToListAsync();
        }

        public async Task<PostViewModel> GetById(string postId)
        {
            Post post = await _postRepository.GetById<Post, string>(postId);

            return new PostViewModel
            {
                Id = post.Id,
                Title = post.Content,
                Excerpt = post.Excerpt,
                Content = post.Content,
                CategoryId = post.CategoryId,
                PostTags = post.PostTags
            };
        }

        public void IncreaseViewCount(PostViewModel post)
        {
            if (post.IsPublished)
            {
                post.ViewCount += 1;
            }
        }

        public async Task<DataResult> Create(PostViewModel viewModel)
        {
            try
            {
                Post post = new Post
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title,
                    Content = viewModel.Content,
                    Excerpt = viewModel.Excerpt,
                    CategoryId = viewModel.CategoryId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Slug = CreateSlug(viewModel.Title),
                    IsPublished = false,
                    PostTags = viewModel.PostTags
                };

                return await _postRepository.Create(post);
            }
            catch (Exception ex)
            {
                return new DataResult
                {
                    Status = Status.Exception,
                    Message = $"{ex.Message}"
                };
            }
        }

        public async Task<DataResult> Update(PostViewModel viewModel)
        {
            try
            {
                Post post = new Post
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title,
                    Content = viewModel.Content,
                    Excerpt = viewModel.Excerpt,
                    CategoryId = viewModel.CategoryId,
                    UpdatedDate = DateTime.Now,
                    Slug = CreateSlug(viewModel.Title),
                    IsPublished = viewModel.IsPublished,
                    PostTags = viewModel.PostTags
                };

                return await _postRepository.Update(post);
            }
            catch (Exception ex)
            {
                return new DataResult
                {
                    Status = Status.Exception,
                    Message = $"{ex.Message}"
                };
            }
        }

        //private async Task<DataResult> RemoveAllTagsByPostId(string postId)
        //{
        //    Post post = await _postRepository.GetById<Post, string>(postId);

        //    return await _postRepository.DeleteBatch(post.PostTags);
        //}

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
