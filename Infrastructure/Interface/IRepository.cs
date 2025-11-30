using System.Linq.Expressions;

namespace Infrastructure.Interface
{
    public interface IRepository<T>
    {
        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(T entity);

        Task<T?> GetByIdAsync(Guid id);

        Task<IEnumerable<T>?> AllAsync();

        Task<T?> FindAsync(Expression<Func<T, object>> expression);

        Task SaveChangesAsync();
    }
}
