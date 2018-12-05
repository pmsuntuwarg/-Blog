using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Areas.Admin.Models;
using Blog.Data;

namespace Blog.Areas.Admin.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;
        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Delete(Tag tag)
        {
            _context.Remove(tag);
            return _context.SaveChanges();
        }
        
        public IEnumerable<Tag> GetAllTags()
        {
            return _context.Tags;
        }

        public Tag GetTagById(string tagId)
        {
            return _context.Tags.FirstOrDefault(t => t.TagId == tagId);
        }

        public int Save(Tag tag)
        {
            _context.Add(tag);
            return _context.SaveChanges();
        }

        public int Update(Tag tag)
        {
            _context.Update(tag);
            return _context.SaveChanges();
        }
    }
}
