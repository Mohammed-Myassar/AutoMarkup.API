using Application.ViewModel.ProjectsViewModel;

namespace Application.Abstractions
{
    public interface IProjectService
    {
        Task<ProjectDto> CreateProjectAsync(CreateProjectRequest request);
        Task<List<ProjectDto>> GetUserProjectsAsync(Guid userId);
        Task<ProjectDetailsDto> GetProjectDetailsAsync(Guid projectId);
        Task<ProjectDto> UpdateProjectAsync(
            Guid projectId, UpdateProjectRequest request);
        Task<bool> DeleteProjectAsync(Guid projectId);
    }
}
