using Domain.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interface
{
    public interface IElementRepository : IRepository<Element>
    {
        Task<Element?> GetByIdIncludingAsync(Guid id,
            params Expression<Func<Element, object>>[] includeProperties);
    }
}
