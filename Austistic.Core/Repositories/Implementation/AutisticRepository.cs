using AlpaStock.Core.Context;
using AlpaStock.Core.Repositories.Interface;




namespace Austistic.Core.Repositories.Implementation
{
    public class AutisticRepository<TEntity> : IAutisticRepository<TEntity> where TEntity : class
    {
        private readonly AustisticContext _context;

        public AutisticRepository(AustisticContext context)
        {
            _context = context;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);
            return result.Entity;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public void Delete(List<TEntity> entity)
        {
            _context.Set<TEntity>().RemoveRange(entity);
        }
        public async Task<TEntity?> GetByIdAsync(string id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }


        public IQueryable<TEntity> GetQueryable()
        {
            return _context.Set<TEntity>();
        }

        public Task<int> SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
