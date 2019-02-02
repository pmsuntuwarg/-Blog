
using Blog.Entities.Models;
using Blog.Entities.ViewModels;

namespace Blog.Infrastructure.Interfaces.Admin
{
    public interface ICommentService : IBaseService<Comment, CommentViewModel, string>
    {
    }
}
