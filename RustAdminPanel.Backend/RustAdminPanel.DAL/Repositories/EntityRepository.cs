using Microsoft.EntityFrameworkCore;
using RustAdminPanel.DAL.Context;

namespace RustAdminPanel.DAL.Repositories
{
    public interface IEntityRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);

        List<T> Get(Func<T, bool> predicate);

        Task<List<T>> GetAllAsync();

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task UpdateAsync(List<T> entities);

        Task DeleteAsync(T entity);

        IQueryable<T> GetQueryable();
    }

    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        private readonly RustAdminPanelContext _context;
        private readonly DbSet<T> _dbSet;

        public EntityRepository(RustAdminPanelContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public List<T> Get(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable<T>();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<T> entities)
        {
            foreach(var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }
    }
}
