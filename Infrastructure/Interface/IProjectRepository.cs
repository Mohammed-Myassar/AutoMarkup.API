using Domain.Entities;

namespace Infrastructure.Interface
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<Project?> GetByIdWithDetailsAsync(Guid projectId);
        Task<List<Project>> GetUserProjectsAsync(Guid userId);
    }
}
