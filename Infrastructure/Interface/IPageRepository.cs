using Domain.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interface
{
    public interface IPageRepository : IRepository<Page>
    {
        Task<Page?> GetByIdIncludingAsync(Guid pageId,
            params Expression<Func<Page, object>>[] includeProperties);
    }
}
