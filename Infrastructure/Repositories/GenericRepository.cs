using Infrastructure.DbContexts;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly AutoMarkupDb context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(AutoMarkupDb context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>?> AllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<T?> FindAsync(Expression<Func<T, object>> expression)
        {
            return await dbSet.FindAsync(expression);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await SaveChangesAsync();
            return entity;
        }
    }
}
