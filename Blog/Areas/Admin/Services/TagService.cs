using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Areas.Admin.Models;
using Blog.Areas.Admin.Repositories;

namespace Blog.Areas.Admin.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public int DeleteTag(Tag tag)
        {
            if ( tag != null)
            {
                return _tagRepository.Delete(tag);
            }

            return 0;
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return _tagRepository.GetAllTags();
        }

        public Tag GetTagById(string tagId)
        {
            return _tagRepository.GetTagById(tagId);
        }

        public int SaveTag(Tag tag, string action = "Create")
        {
            if(action == "Update")
            {
                return _tagRepository.Update(tag);
            }

            return _tagRepository.Save(tag);
        }
    }
}
