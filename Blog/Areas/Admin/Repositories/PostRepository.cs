using Blog.Areas.Admin.Models;
using Blog.Data;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Areas.Admin.Repositories
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
            var whereQuery = _context.Posts.Where(p => p.Content.Contains(query) || p.Excerpt.Contains(query)|| p.Title.Contains(query))
                .Include(p => p.Category)
                .Include(p => p.Comments)
                .Include(p=>p.PostTags).ThenInclude(pt => pt.Tags);
            PagedList<Post> posts = new PagedList<Post>(whereQuery, pageNumber, pageSize);

            return posts;
        }

        public IEnumerable<Post> RecentPosts(int takeTotal = 5)
        {
            return _context.Posts
                .OrderBy(p => p.CreatedAt)
                .Take(takeTotal);
        }

        public Post GetPostById(string postId)
        {
            return _context.Posts
                .Include(p => p.Comments)
                .Include( p => p.Category)
                .Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tags)
                .FirstOrDefault(p => p.PostId == postId);
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

        public void DeletePostTags(IEnumerable<PostTag> postTags)
        {
            foreach(PostTag postTag in postTags)
            {
                _context.PostTags.Remove(postTag);
            }

            _context.SaveChanges();
        }
    }
}
