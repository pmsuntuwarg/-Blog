using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Entities.Models;

namespace Blog.Infrastructure.Interfaces.Admin
{
    public interface IMediaRepository : IBaseRepository
    {
        List<Media> GetMedias(Expression<Func<Media, bool>> whereClause, string searchBy, int take, int skip, string sortBy, bool sortDir);
        Task<Media> GetHomeImage();
    }
}
