using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Repositories;
using Blog.ViewModels;

namespace Blog.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public string DeleteComment(string id)
        {
            Comment comment = _commentRepository.GetCommentById(id);
            _commentRepository.DeleteComment(comment);

            return "Deleted";
        }

        public bool UpdateComment(CommentViewModel commentViewModel)
        {
            try
            {
                Comment comment = _commentRepository.GetCommentById(commentViewModel.Id);
                comment.Content = commentViewModel.Content;
                _commentRepository.UpdateComment(comment);

                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        public bool SaveComment(CommentViewModel commentViewModel)
        {
            try
            {
                Comment comment = new Comment();
                comment.Author = "Anonymous";
                comment.Content = commentViewModel.Content;
                comment.PostId = commentViewModel.PostId;
                _commentRepository.SaveComment(comment);

                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }
    }
}
