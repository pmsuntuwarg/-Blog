﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Common.Enums;
using Blog.Entities.Models;
using Blog.Entities;
using Blog.Infrastructure.Interfaces.Admin;
using Blog.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await _tagRepository.GetAllAsync<Tag>().ToListAsync();
        }

        public Task<Tag> GetById(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<Tag> GetTagById(string tagId)
        {
            return await _tagRepository.GetById<Tag, string>(tagId);
        }

        public async Task<DataResult> Create(Tag tag)
        {
            return await _tagRepository.Create(tag);
        }

        public async Task<DataResult> Update(Tag viewModel)
        {
            return await _tagRepository.Update(viewModel);
        }
    }
}