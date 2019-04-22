using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Interfaces
{
    public interface IBaseRepository
    {
        Task<DataResult> Create<T>(T model) where T : class;
        Task<DataResult> Update<T>(T model) where T : class;
        Task<DataResult> Delete<T>(T model) where T : class;

        Task<DataResult> CreateBatch<T>(List<T> model) where T : class;
        Task<DataResult> UpdateBatch<T>(List<T> model) where T : class;
        Task<DataResult> DeleteBatch<T>(List<T> model) where T : class;

        Task<DataResult> DeleteUpdate<TDelete, TUpdate>(TDelete dModel, TUpdate uModel) where TDelete : class where TUpdate : class;
        Task<DataResult> DeleteSave<TDelete, TSave>(TDelete dModel, TSave sModel) where TDelete : class where TSave : class;

        Task<DataResult> DeleteBatchUpdate<TDelete, TUpdate>(List<TDelete> dModel, TUpdate uModel) where TDelete : class where TUpdate : class;
        Task<DataResult> DeleteBatchSave<TDelete, TSave>(List<TDelete> dModel, TSave sModel) where TDelete : class where TSave : class;

        IEnumerable<T> GetAll<T>() where T : class;
        IQueryable<T> GetAllAsync<T>() where T : class;
        Task<T> GetById<T, TKey>(TKey key) where T : class;
    }
}
