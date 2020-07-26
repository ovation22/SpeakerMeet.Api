using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SpeakerMeet.Infrastructure.Data.Repositories
{
    public abstract class EFRepository
    {
        private readonly SpeakerMeetContext _context;

        protected EFRepository(SpeakerMeetContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            var dbSet = _context.Set<T>();

            return await dbSet.ToListAsync();
        }

        public async Task<T> Add<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task Update<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
