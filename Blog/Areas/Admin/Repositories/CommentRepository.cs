using System.Linq;
using Blog.Areas.Admin.Models;
using Blog.Data;

namespace Blog.Areas.Admin.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);

            return _context.SaveChanges();
        }

        public Comment GetCommentById(string id)
        {
            return _context.Comments.FirstOrDefault(c => c.CommentId == id);
        }

        public int SaveComment(Comment comment)
        {
            _context.Comments.Add(comment);

            return _context.SaveChanges();
        }

        public int UpdateComment(Comment comment)
        {
            _context.Comments.Update(comment);

            return _context.SaveChanges();
        }
    }
}
