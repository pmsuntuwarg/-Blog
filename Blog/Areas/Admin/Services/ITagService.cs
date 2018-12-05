using Blog.Areas.Admin.Models;
using System.Collections.Generic;

namespace Blog.Areas.Admin.Services
{
    public interface ITagService
    {
        Tag GetTagById(string tagId);
        IEnumerable<Tag> GetAllTags();
        int SaveTag(Tag tag, string action="Create");
        int DeleteTag(Tag tag);
    }
}
