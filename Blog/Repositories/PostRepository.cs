using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }

        public IEnumerable<Post> GetAllPosts(string query, int pageNumber = 1, int pageSize = 10)
        {
            var whereQuery = _context.Posts.Where(p => p.Content.Contains(query) || p.Excerpt.Contains(query)|| p.Title.Contains(query));
            PagedList<Post> posts = new PagedList<Post>(whereQuery, pageNumber, pageSize);
            //return _context.Posts.Where(p => string.Compare(p.Content,query,StringComparison.OrdinalIgnoreCase)>0);
            //return _context.Posts.Where(p => p.Content.Contains(query) || p.Excerpt.Contains(query));

            return posts;
        }

        public Post GetPostById(string postId)
        {
            return _context.Posts.Include(p => p.Comments).FirstOrDefault(p => p.PostId == postId);
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
