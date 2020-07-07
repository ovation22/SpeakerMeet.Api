using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpeakerMeet.Core.Interfaces.Repositories
{
    public interface IEFRepository
    {
        Task<T> Get<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<IEnumerable<T>> GetAll<T>() where T : class;
        Task<IEnumerable<T>> GetAll<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<IEnumerable<T>> GetAll<T>(Expression<Func<T, bool>> expression, Expression<Func<T, object>> include) where T : class;
        Task<T> Add<T>(T entity) where T : class;
        Task Update<T>(T entity) where T : class;
        Task Delete<T>(T entity) where T : class;
    }
}
