using Blog.Models;
using Blog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services
{
    public interface ICommentService
    {
        bool SaveComment(CommentViewModel commentViewModel);
        bool UpdateComment(CommentViewModel commentViewModel);
        string DeleteComment(string Id);
    }
}
