using Domain.Entities;
using Infrastructure.DbContexts;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class PageRepository : GenericRepository<Page>, IPageRepository
    {
        private readonly AutoMarkupDb context;
        private readonly DbSet<Page> dbSet;
        public PageRepository(AutoMarkupDb context) : base(context)
        {
            this.context = context;
            dbSet = context.Set<Page>();
        }

        public async Task<Page?> GetByIdIncludingAsync(Guid id,
            params Expression<Func<Page, object>>[] includeProperties)
        {
            IQueryable<Page> query = dbSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (!includeProperties.Any())
                return await dbSet.FindAsync(id);

            return await query.FirstOrDefaultAsync(e => e.PageId == id);
        }
    }
}
