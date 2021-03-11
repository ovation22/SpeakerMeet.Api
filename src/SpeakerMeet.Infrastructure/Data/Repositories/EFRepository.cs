using System.Collections.Generic;
using System.Linq;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpeakerMeet.Core.Interfaces.Repositories;

namespace SpeakerMeet.Infrastructure.Data.Repositories
{
    public abstract class EFRepository : IEFRepository
    {
        private readonly DbContext _context;

        protected EFRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<T> Get<T>(ISpecification<T> spec) where T : class
        {
            var specificationResult = ApplySpecification(spec);

            return await specificationResult.SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> List<T>(ISpecification<T> spec) where T : class
        {
            var specificationResult = ApplySpecification(spec);

            return await specificationResult.ToListAsync();
        }

        public async Task<int> Count<T>() where T : class
        {
            var dbSet = _context.Set<T>();

            return await dbSet.CountAsync();
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

        private IQueryable<T> ApplySpecification<T>(ISpecification<T> spec) where T : class
        {
            var evaluator = new SpecificationEvaluator();

            return evaluator.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
