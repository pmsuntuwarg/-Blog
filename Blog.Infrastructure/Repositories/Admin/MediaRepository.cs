using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Data.DbContext;
using Blog.Entities.Models;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories.Admin
{
    public class MediaRepository : BaseRepository, IMediaRepository
    {
        private readonly ApplicationDbContext _context;
        public MediaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Media> GetHomeImage()
        {
            return await _context.Medias.Where(m => m.IsHomeImage).OrderByDescending(m => m.CreatedDate).FirstOrDefaultAsync();
        }

        public List<Media> GetMedias(Expression<Func<Media, bool>> whereClause, string searchBy, int take, int skip, string sortBy, bool sortDir)
        {
            if (string.IsNullOrEmpty(searchBy))
            {
                // if we have an empty search then just order the results by Id ascending
                sortBy = "Id";
                sortDir = true;
            }

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            int totalCount = _context.Medias.Count();//(m=>m.TotalCount =  _context.Pages.Count());
            int filteredCount = _context.Medias.Count(whereClause);//(m=>m.TotalCount =  _context.Pages.Count());

            var result = _context.Medias
                           //.AsExpandable()
                           .Where(whereClause)
                           .Select(m => new Media
                           {
                               Id            = m.Id,
                               FileName      = m.FileName,
                               FileUrl       = m.FileUrl,
                               PreviewUrl    = m.PreviewUrl,
                               FileType      = m.FileType,
                               CreatedDate   = m.CreatedDate,
                               UpdatedDate   = m.UpdatedDate,
                               TotalCount    = totalCount,
                               FilteredCount = filteredCount
                           })
                           //.OrderBy(sortBy, sortDir) // have to give a default order when skipping .. so use the PK
                           .Skip(skip)
                           .Take(take)
                           .ToList();

            return result;
        }
    }
}
