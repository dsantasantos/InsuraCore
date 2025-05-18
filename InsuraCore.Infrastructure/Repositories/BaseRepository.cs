using InsuraCore.Domain.Entities;
using InsuraCore.Domain.Interfaces;
using InsuraCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InsuraCore.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly InsuraCoreDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(InsuraCoreDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
} 