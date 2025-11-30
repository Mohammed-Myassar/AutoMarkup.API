using Application.ViewModel.SettingsViewModel;

namespace Application.ViewModel.ProjectsViewModel
{
    public class ProjectDto
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int PagesCount { get; set; }
        public SettingsDto? Settings { get; set; }
    }
}
