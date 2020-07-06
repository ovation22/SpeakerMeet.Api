using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpeakerMeet.Core.Interfaces.Repositories;

namespace SpeakerMeet.Infrastructure.Data.Repositories
{
    public class SpeakerMeetRepository : ISpeakerMeetRepository
    {
        private readonly SpeakerMeetContext _context;

        protected SpeakerMeetRepository(SpeakerMeetContext context)
        {
            _context = context;
        }

        public async Task<T> Get<T>(Expression<Func<T, bool>> expression) where T : class
        {
            var dbSet = _context.Set<T>();

            return await dbSet.SingleOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            var dbSet = _context.Set<T>();

            return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>(Expression<Func<T, bool>> expression) where T : class
        {
            var dbSet = _context.Set<T>();

            return await dbSet.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>(Expression<Func<T, bool>> expression, Expression<Func<T, object>> include) where T : class
        {
            var dbSet = _context.Set<T>();

            return await dbSet.Include(include).Where(expression).ToListAsync();
        }
    }
}
