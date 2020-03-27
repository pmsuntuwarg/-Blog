using Blog.Common.Enums;
using Blog.Common.Helpers;
using Blog.Entities;
using Blog.Entities.Models;
using Blog.Entities.ViewModels;
using Blog.Entities.ViewModels.DataTable;
using Blog.Infrastructure.Interfaces.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services.Admin
{
    public class MediaService: IMediaService
    {
        public IMediaRepository _mediaRepository;
        public IHttpContextAccessor _contextAccesor;
        public MediaService(IHttpContextAccessor contextAccesor, IMediaRepository mediaRepository)
        {
            _contextAccesor  = contextAccesor;
            _mediaRepository = mediaRepository;
        }
        public async Task<DataResult> Create(MediaViewModel viewModel)
        {
            DataResult result = new DataResult();
            try
            {
                if (viewModel.UploadedFile == null || viewModel.UploadedFile.Length == 0)
                {
                    result.Status = Status.Failed;
                    return result;
                }

                string   [] allowedContentType = { "image/jpg", "image/png", "image/jpeg" };
                IFormFile file                 = viewModel.UploadedFile;
                if (!allowedContentType.Contains(file.ContentType.ToLowerInvariant()))
                {
                    result.Status = Status.Failed;
                    return result;
                }

                //saving file
                Guid   guid      = Guid.NewGuid();
                string directory = $"wwwroot\\files\\{_contextAccesor.HttpContext.User.Identity.Name}\\{DateTime.Now.Year}\\{DateTime.Now.Month}";
                 string previewDirectory = $"wwwroot\\files\\{_contextAccesor.HttpContext.User.Identity.Name}\\{DateTime.Now.Year}\\{DateTime.Now.Month}\\preview";

                string path = Path.Combine(directory, $"{guid.ToString()}{file.FileName}");
                string previewPath = Path.Combine(previewDirectory, $"{guid.ToString()}{file.FileName}");
                if(!Directory.Exists(directory)) Directory.CreateDirectory(directory);
                if(!Directory.Exists(previewDirectory)) Directory.CreateDirectory(previewDirectory);

                string imageUrl = $"/files/{_contextAccesor.HttpContext.User.Identity.Name}/{DateTime.Now.Year}/{DateTime.Now.Month}/{guid.ToString()}{file.FileName}";
                string previewImageUrl = $"/files/{_contextAccesor.HttpContext.User.Identity.Name}/{DateTime.Now.Year}/{DateTime.Now.Month}/preview/{guid.ToString()}{file.FileName}";

                using (var stream = new FileStream(path, FileMode.Create)) await file.CopyToAsync(stream);
                
                    var image = Image.FromFile(path);
                    int width, height, size = 70;
                    if (image.Width > image.Height)
                    {
                        width = size;
                        height = Convert.ToInt32(image.Height * size / (double)image.Width);
                    }
                    else
                    {
                        width = Convert.ToInt32(image.Width * size / (double)image.Height);
                        height = size;
                    }
                    var thumbnail = image.GetThumbnailImage(size, height, null, IntPtr.Zero);
                    thumbnail.Save(previewPath, ImageFormat.Jpeg);

                Media media = new Media
                {
                    FileName = $"{guid}{file.FileName}",
                    FileUrl  = imageUrl,
                    FileType = file.ContentType,
                    PreviewUrl = previewImageUrl,
                    IsHomeImage = viewModel.IsHomeImage,
                    CreatedDate = DateTime.Now
                };

                result = await _mediaRepository.Create(media);
                if(result.Status == Status.Failed)
                {
                    File.Delete(path);
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Status  = Status.Failed;
                result.Message = ex.Message;

                return result;
            }
        }

        public Task<DataResult> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<MediaViewModel>> GetAll(string searchQuery = "")
        {
             return await (from result in _mediaRepository.GetAllAsync<Media>()
                          select new MediaViewModel
                          {
                            Id       = result.Id,
                            FileName = result.FileName,
                            FileUrl  = result.FileUrl,
                            PreviewUrl = result.PreviewUrl
                          }).ToListAsync();
        }

        public async Task<MediaViewModel> GetById(string key)
        {
            Media media = await _mediaRepository.GetById<Media, string>(key);

            MediaViewModel mediaViewModel = new MediaViewModel
            {
                Id       = media.Id,
                FileName = media.FileName,
                FileUrl  = media.FileUrl,
                PreviewUrl = media.PreviewUrl
            };

            return mediaViewModel;
        }

        public async Task<List<MediaViewModel>> GetMedias(DataTableAjaxModel model)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;

            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }

            if (string.IsNullOrEmpty(searchBy))
            {
                // if we have an empty search then just order the results by Id ascending
                sortBy = "Id";
                sortDir = true;
            }

            var whereClause = BuildDynamicWhereClause(searchBy);
            // search the dbase taking into consideration table sorting and paging
            List<MediaViewModel> result = (from media in _mediaRepository.GetMedias(whereClause, searchBy, take, skip, sortBy, sortDir)
                                          select new MediaViewModel
                                          {
                                            Id       = media.Id,
                                            FileName = media.FileName,
                                            FileUrl  = media.FileUrl,
                                            PreviewUrl = media.PreviewUrl,
                                            TotalCount = media.TotalCount,
                                            FilteredCount = media.FilteredCount
                                          }).ToList();

            // if (result.Count() == 0)
            // {
            //     // empty collection...
            //     return new List<PageViewModel>();
            // }

            return result;
        }

       private Expression<Func<Media, bool>> BuildDynamicWhereClause(string searchValue)
        {
            // simple method to dynamically plugin a where clause
            var predicate = PredicateBuilder.True<Media>();  // true -where(true) return all

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchValue.Split(' ').ToList().ConvertAll(x => x.ToLower());

                predicate = predicate.Or(s => searchTerms.Any(srch => s.FileName.ToLower().Contains(srch)));
            }

            return predicate;
        }

        public async Task<PaginatedList<MediaViewModel>> GetPaginatedList(int? page, int? pageSize)
        {
            return await PaginatedList<MediaViewModel>.CreateAsync((from result in _mediaRepository.GetAllAsync<Media>()
            select new MediaViewModel
            {
                Id       = result.Id,
                FileName = result.FileName,
                FileUrl  = result.FileUrl,
                PreviewUrl = result.PreviewUrl
            }).AsNoTracking().OrderByDescending(m => m.CreatedDate), page ?? 1, pageSize ?? 10);
        }

        public Task<DataResult> Update(MediaViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResult> GetHomeImage()
        {
            Media media = await _mediaRepository.GetHomeImage();

            return new DataResult { Status = Status.Success, Message=media.FileUrl };
        }
    }
}
