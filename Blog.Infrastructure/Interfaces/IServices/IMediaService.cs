using Blog.Entities;
using Blog.Entities.Models;
using Blog.Entities.ViewModels;
using Blog.Entities.ViewModels.DataTable;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Interfaces.Admin
{
    public interface IMediaService : IBaseService<Media, MediaViewModel, string>
    {
        Task<List<MediaViewModel>> GetMedias(DataTableAjaxModel model);
        Task<DataResult> GetHomeImage();
    }
}
