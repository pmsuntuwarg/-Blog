using System.Linq;
using Blog.Data;
using Blog.Data.DbContext;
using Blog.Entities.Models;
using Blog.Infrastructure.Interfaces.Admin;

namespace Blog.Infrastructure.Repositories.Admin
{
    public class CommentRepository :BaseRepository, ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
