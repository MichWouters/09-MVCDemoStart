

namespace MVCDemo.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly MVCDemoContext _context;

        public GenericRepository(MVCDemoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
             await _context.Set<TEntity>().AddAsync(entity);
          }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            return entity != null;
        }

        public void RemoveRange(List<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
