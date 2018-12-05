using Blog.Areas.Admin.Models;

namespace Blog.Areas.Admin.Repositories
{
    public interface ICommentRepository
    {
        Comment GetCommentById(string id);
        int SaveComment(Comment comment);
        int DeleteComment(Comment comment);
        int UpdateComment(Comment comment);
    }
}
