using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models;

namespace Blog.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeletePost(string postId)
        {
            Post post = GetPostById(postId);
            if(post != null)
                _context.Posts.Remove(post);
        }

        public IEnumerable<Post> GetAllPosts(string query)
        { 
            //return _context.Posts.Where(p => string.Compare(p.Content,query,StringComparison.OrdinalIgnoreCase)>0);
            return _context.Posts.Where(p => p.Content.Contains(query) || p.Excerpt.Contains(query));
        }

        public Post GetPostById(string postId)
        {
            return _context.Posts.FirstOrDefault(p => p.PostId == postId);
        }

        public void SavePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void UpdatePost(Post updatedPost)
        {
            _context.Posts.Update(updatedPost);
            _context.SaveChanges();
        }
    }
}
