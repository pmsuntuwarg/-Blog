using Blog.Common.Enums;
using Blog.Common.Helpers;
using Blog.Entities;
using Blog.Entities.Models;
using Blog.Entities.Models.Identity;
using Blog.Entities.ViewModels;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services.Admin
{
    public class PostService: IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostService(IPostRepository postRepository, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _postRepository      = postRepository;
            _userManager         = userManager;
            _httpContextAccessor = httpContextAccessor;
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
                Status  = Status.Failed,
                Message = "Cannot delete"
            };
        }

        public async Task<IReadOnlyList<PostViewModel>> GetAll(string searchQuery)
        {
                searchQuery   = searchQuery?.ToLowerInvariant();
            var allPostsQuery = _postRepository.GetAllAsync<Post>();

            if (!string.IsNullOrEmpty(searchQuery?.Trim()))
                allPostsQuery = allPostsQuery.Where(x => x.Title.ToLowerInvariant().Contains(searchQuery) || x.Content.ToLowerInvariant().Contains(searchQuery));

            return await (from result in allPostsQuery
                          select new PostViewModel
                          {
                              Id           = result.Id,
                              Title        = result.Title,
                              Excerpt      = result.Excerpt,
                              Slug         = result.Slug,
                              Content      = result.Content,
                              CategoryId   = result.CategoryId,
                              PostTags     = result.PostTags,
                              CommentCount = result.Comments.Count(),
                              ViewCount    = result.ViewCount,
                              CreatedDate  = result.CreatedDate,
                              Author       = result.CreatedBy.FullName
                          }).OrderByDescending(m => m.CreatedDate).ToListAsync();
        }

        public async Task<PostViewModel> GetById(string postId)
        {
            Post post = await _postRepository.GetById(postId);

            return new PostViewModel
            {
                Id           = post.Id,
                Title        = post.Title,
                Excerpt      = post.Excerpt,
                Slug         = post.Slug,
                Content      = post.Content,
                CategoryId   = post.CategoryId,
                PostTags     = post.PostTags,
                CreatedDate  = post.CreatedDate,
                CommentCount = post.Comments.Count(),
                ViewCount    = post.ViewCount,
                Author       = post.CreatedBy.FullName
            };
        }
        public async Task<PostViewModel> GetBySlug(string slug)
        {
            Post post = await _postRepository.GetBySlug(slug);

            return new PostViewModel
            {
                Id           = post.Id,
                Title        = post.Title,
                Excerpt      = post.Excerpt,
                Slug         = post.Slug,
                Content      = post.Content,
                CategoryId   = post.CategoryId,
                PostTags     = post.PostTags,
                CreatedDate  = post.CreatedDate,
                CommentCount = post.Comments.Count(),
                ViewCount    = post.ViewCount,
                Author       = post.CreatedBy.FullName
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
                    Id          = viewModel.Id,
                    Title       = viewModel.Title,
                    Content     = viewModel.Content,
                    Excerpt     = viewModel.Excerpt,
                    CategoryId  = viewModel.CategoryId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    CreatedById = _userManager.GetUserId(_httpContextAccessor.HttpContext.User),
                    Slug        = CreateSlug(viewModel.Title),
                    IsPublished = false
                };

                foreach (var tagId in viewModel.TagIds)
                {
                    post.PostTags.Add(new PostTag { TagId = tagId });
                }

                return await _postRepository.Create(post);
            }
            catch (Exception ex)
            {
                return new DataResult
                {
                    Status  = Status.Exception,
                    Message = $"{ex.Message}"
                };
            }
        }

        public async Task<DataResult> Update(PostViewModel viewModel)
        {
            try
            {
                Post post = await _postRepository.GetById(viewModel.Id);

                if (post == null) throw new Exception("post doestn't Exist");

                post.Id          = viewModel.Id;
                post.Slug        = post.Title.ToLowerInvariant() == viewModel.Title.ToLowerInvariant() ? post.Slug : CreateSlug(viewModel.Title);
                post.Title       = viewModel.Title;
                post.Content     = viewModel.Content;
                post.Excerpt     = viewModel.Excerpt;
                post.CategoryId  = viewModel.CategoryId;
                post.UpdatedDate = DateTime.Now;
                post.IsPublished = viewModel.IsPublished;

                await _postRepository.DeleteBatch(post.PostTags.ToList());

                foreach (var tagId in viewModel.TagIds)
                {
                    post.PostTags.Add(new PostTag { TagId = tagId });
                }

                return await _postRepository.Update(post);
            }
            catch (Exception ex)
            {
                return new DataResult
                {
                    Status  = Status.Exception,
                    Message = $"{ex.Message}"
                };
            }
        }

        private string CreateSlug(string title)
        {
            var slug = title.ToLowerInvariant().Trim().Replace(" ", "-");
            return slug;
        }

        public int GetCountByCategory(string categoryName)
        {
            throw new NotImplementedException();
            // return _postRepository.GetCountByCategory(categoryName);
        }

        public async Task<PaginatedList<PostViewModel>> GetPaginatedList(int? page, int? pageSize)
        {
            return await PaginatedList<PostViewModel>.CreateAsync((from result in _postRepository.GetAllAsync<Post>()
                                                                   select new PostViewModel
                                                                   {
                                                                       Id          = result.Id,
                                                                       Title       = result.Title,
                                                                       Excerpt     = result.Excerpt,
                                                                       Slug        = result.Slug,
                                                                       Content     = result.Content,
                                                                       CategoryId  = result.CategoryId,
                                                                       PostTags    = result.PostTags,
                                                                       CreatedDate = result.CreatedDate,
                                                                   }).AsNoTracking().OrderByDescending(m => m.CreatedDate), page ?? 1, pageSize ?? 10);
        }
    }
}
