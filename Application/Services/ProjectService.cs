using Application.Abstractions;
using Application.ViewModel.ProjectsViewModel;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interface;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;
        private readonly IAccountRepository userRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ProjectService> logger;

        public ProjectService(
            IProjectRepository projectRepository,
            IAccountRepository userRepository,
            IMapper mapper,
            ILogger<ProjectService> logger)
        {
            this.projectRepository = projectRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ProjectDto> CreateProjectAsync(CreateProjectRequest request)
        {
            var user = await userRepository.GetByIdAsync(request.UserId);

            if (user == null)
                throw new ArgumentException("User not found");

            var project = mapper.Map<Project>(request);
            var createdProject = await projectRepository.AddAsync(project);

            logger.LogInformation($"Project created: {createdProject.ProjectId} by user {request.UserId}");

            return mapper.Map<ProjectDto>(createdProject);
        }

        public async Task<List<ProjectDto>> GetUserProjectsAsync(Guid userId)
        {
            var projects = await projectRepository.GetUserProjectsAsync(userId);
            return mapper.Map<List<ProjectDto>>(projects);
        }

        public async Task<ProjectDetailsDto> GetProjectDetailsAsync(Guid projectId)
        {
            var project = await projectRepository.GetByIdWithDetailsAsync(projectId);

            if (project == null)
                throw new ArgumentException("Project not found");

            return mapper.Map<ProjectDetailsDto>(project);
        }

        public async Task<ProjectDto> UpdateProjectAsync(
            Guid projectId, UpdateProjectRequest request)
        {
            var project = await projectRepository.GetByIdAsync(projectId);

            if (project == null) 
                throw new ArgumentException("Project not found");

            mapper.Map(request, project);
            project.UpdatedAt = DateTime.UtcNow;

            var updatedProject = await projectRepository.UpdateAsync(project);
            return mapper.Map<ProjectDto>(updatedProject);
        }

        public async Task<bool> DeleteProjectAsync(Guid projectId)
        {
            var project = await projectRepository.GetByIdAsync(projectId);

            if (project == null)
                throw new ArgumentException("Project not found");

            await projectRepository.DeleteAsync(project);
            logger.LogInformation($"Project deleted: {projectId}");
            return true;
        }
    }
}
