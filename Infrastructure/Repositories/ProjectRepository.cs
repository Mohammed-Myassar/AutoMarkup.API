using Domain.Entities;
using Infrastructure.DbContexts;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        private readonly AutoMarkupDb context;

        public ProjectRepository(AutoMarkupDb context) : base(context)
        {
            this.context = context;
        }

        public async Task<Project?> GetByIdWithDetailsAsync(Guid projectId)
        {
            return await context.Projects
                .Include(p => p.User)
                .Include(p => p.Pages!)
                    .ThenInclude(page => page.Elements)
                .Include(p => p.StyleRules)
                .Include(p => p.Settings)
                .FirstOrDefaultAsync(p => p.ProjectId == projectId);
        }

        public async Task<List<Project>> GetUserProjectsAsync(Guid userId)
        {
            return await context.Projects
                .Where(p => p.UserId == userId)
                .Include(p => p.Pages)
                .Include(p => p.Settings)
                .OrderByDescending(p => p.UpdatedAt)
                .ToListAsync();
        }
    }
}
