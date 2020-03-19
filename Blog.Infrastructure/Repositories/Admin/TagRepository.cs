using Blog.Data.DbContext;
using Blog.Infrastructure.Interfaces.IRepositories;

namespace Blog.Infrastructure.Repositories.Admin
{
    public class TagRepository : BaseRepository, ITagRepository
    {
        private readonly ApplicationDbContext _context;
        public TagRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
