using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpeakerMeet.Core.Interfaces.Repositories
{
    public interface ISpeakerMeetRepository : IEFRepository
    {
        Task<int> Count<T>() where T : class;
        Task<IEnumerable<T>> GetRandom<T>(int count) where T : class; 
        Task<IEnumerable<T>> GetAll<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<IEnumerable<T>> GetAll<T>(Expression<Func<T, bool>> expression, Expression<Func<T, object>> include) where T : class;
    }
}
