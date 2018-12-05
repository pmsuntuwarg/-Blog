using Blog.Areas.Admin.ViewModels;

namespace Blog.Areas.Admin.Services
{
    public interface ICommentService
    {
        bool SaveComment(CommentViewModel commentViewModel);
        bool UpdateComment(CommentViewModel commentViewModel);
        string DeleteComment(string Id);
    }
}
