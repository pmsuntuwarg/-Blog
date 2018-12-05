using Blog.Areas.Admin.Models;
using System.Collections.Generic;

namespace Blog.Areas.Admin.Repositories
{
    public interface ITagRepository
    {
        int Save(Tag tag);
        int Update(Tag tag);
        int Delete(Tag tag);
        IEnumerable<Tag> GetAllTags();
        Tag GetTagById(string tagId);
    }
}
