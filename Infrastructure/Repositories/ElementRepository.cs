using Domain.Entities;
using Infrastructure.DbContexts;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class ElementRepository : GenericRepository<Element>, IElementRepository
    {
        private readonly AutoMarkupDb context;
        private readonly DbSet<Element> dbSet;

        public ElementRepository(AutoMarkupDb context) : base(context)
        {
            this.context = context;
            dbSet = context.Set<Element>();
        }

        public async Task<Element?> GetByIdIncludingAsync(Guid id,
            params Expression<Func<Element, object>>[] includeProperties)
        {
            IQueryable<Element> query = dbSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (!includeProperties.Any())
                return await dbSet.FindAsync(id);

            return await query.FirstOrDefaultAsync(e => e.ElementId == id);
        }
    }
}
