using Blog.Common.Enums;
using Blog.Common.Helpers;
using Blog.Entities;
using Blog.Entities.Models;
using Blog.Infrastructure.Interfaces.Admin;
using Blog.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services.Admin
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<DataResult> Delete(string id)
        {
            Tag tag = await _tagRepository.GetById<Tag, string>(id);

            if (tag != null)
            {
                return await _tagRepository.Delete(tag);
            }

            return new DataResult
            {
                Status = Status.Failed,
                Message = "Not found"
            };
        }

        public async Task<IReadOnlyList<Tag>> GetAll(string searchQuery)
        {
            return await _tagRepository.GetAllAsync<Tag>().ToListAsync();
        }

        public Task<Tag> GetById(string key)
        {
            return _tagRepository.GetById<Tag, string>(key);
        }

        public async Task<DataResult> Create(Tag tag)
        {
            return await _tagRepository.Create(tag);
        }

        public async Task<DataResult> Update(Tag viewModel)
        {
            return await _tagRepository.Update(viewModel);
        }

        public Task<PaginatedList<Tag>> GetPaginatedList(int? page, int? pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
