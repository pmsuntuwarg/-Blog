using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Common.Enums;
using Blog.Entities.Models;
using Blog.Entities.ViewModels;
using Blog.Entities;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Services.Admin
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<DataResult> Delete(string id)
        {
            Comment comment = await _commentRepository.GetById<Comment, string>(id);

            return await _commentRepository.Delete(comment);
        }

        public async Task<DataResult> Update(CommentViewModel viewModel)
        {
            Comment comment = await _commentRepository.GetById<Comment, string>(viewModel.Id);
            comment.Content = viewModel.Content;

            return await _commentRepository.Update(comment);
        }

        public Task<DataResult> Create(CommentViewModel viewModel)
        {
                Comment comment = new Comment();
                comment.Content = viewModel.Content;
                comment.PostId = viewModel.PostId;

                return _commentRepository.Create(comment);
            
        }

        public async Task<IEnumerable<CommentViewModel>> GetAll()
        {
            return await (from result in _commentRepository.GetAllAsync<Comment>()
                         select new CommentViewModel
                         {
                             Id = result.Id,
                             Content = result.Content,
                             ParentId = result.ParentId,
                             PostId = result.PostId
                         }).ToListAsync();
        }

        public async Task<CommentViewModel> GetById(string key)
        {
            Comment result = await _commentRepository.GetById<Comment, string>(key);

            return new CommentViewModel
            {
                Id = result.Id,
                Content = result.Content,
                ParentId = result.ParentId,
                PostId = result.PostId
            };
        }
    }
}
