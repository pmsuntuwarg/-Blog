﻿using Blog.Data.DbContext;
using Blog.Entities.Models;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories.Admin
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Post> GetAll(string query, int pageNumber = 1, int pageSize = 10)
        {
            var whereQuery = _context.Posts.Where(p => p.Content.Contains(query) || p.Excerpt.Contains(query)|| p.Title.Contains(query))
                .Include(p => p.Category)
                .Include(p => p.Comments)
                .Include(p=>p.PostTags).ThenInclude(pt => pt.Tag);

            PagedList<Post> posts = new PagedList<Post>(whereQuery, pageNumber, pageSize);

            return posts;
        }

        public IEnumerable<Post> RecentPosts(int takeTotal = 5)
        {
            return _context.Posts
                .OrderBy(p => p.CreatedDate)
                .Take(takeTotal);
        }

        public async Task<Post> GetById(string postId)
        {
            var a = await _context.Posts
                .Include(p => p.Comments)
                .Include( p => p.Category)
                .Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(p => p.Id == postId);

            return a;
        }


    }
}
