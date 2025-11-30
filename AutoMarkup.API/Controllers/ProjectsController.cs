using Application.Abstractions;
using Application.ViewModel.ProjectsViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarkup.API.Controllers
{
    [Route("api/users/{userId:guid}/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService projectService;
        private readonly ILogger<ProjectsController> logger;

        public ProjectsController(
            IProjectService projectService,
            ILogger<ProjectsController> logger)
        {
            this.projectService = projectService;
            this.logger = logger;
        }

        [HttpPost("add-project")]
        public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] CreateProjectRequest request)
        {
            try
            {
                var project = await projectService.CreateProjectAsync(request);
                return Ok(project);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-user-projects")]
        public async Task<ActionResult<List<ProjectDto>>> GetUserProjects(Guid userId)
        {
            var projects = await projectService.GetUserProjectsAsync(userId);
            return Ok(projects);
        }

        [HttpGet("get-project/{projectId:guid}")]
        public async Task<ActionResult<ProjectDetailsDto>> GetProject(Guid projectId)
        {
            try
            {
                var project = await projectService.GetProjectDetailsAsync(projectId);
                return Ok(project);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("update-project/{projectId:guid}")]
        public async Task<ActionResult<ProjectDto>> UpdateProject(Guid projectId, [FromBody] UpdateProjectRequest request)
        {
            try
            {
                var project = await projectService.UpdateProjectAsync(projectId, request);
                return Ok(project);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("delete-project/{projectId:guid}")]
        public async Task<ActionResult> DeleteProject(Guid projectId)
        {
            try
            {
                await projectService.DeleteProjectAsync(projectId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
