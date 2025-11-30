using Domain.Entities;
using Infrastructure.DbContexts;
using Infrastructure.Interface;

namespace Infrastructure.Repositories
{
    public class StyleRuleRepository : GenericRepository<StyleRule>, IStyleRuleRepository
    {
        public StyleRuleRepository(AutoMarkupDb context) : base(context)
        {
        }
    }
}
