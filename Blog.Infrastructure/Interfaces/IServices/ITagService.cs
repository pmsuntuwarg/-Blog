using Blog.Entities.Models;
using System.Collections.Generic;

namespace Blog.Infrastructure.Interfaces.Admin
{
    public interface ITagService : IBaseService<Tag, Tag, string>
    {
    }
}
