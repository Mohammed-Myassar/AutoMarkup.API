using Application.ViewModel.SettingsViewModel;

namespace Application.ViewModel.ProjectsViewModel
{
    public class CreateProjectRequest
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required Guid UserId { get; set; }
        public CreateSettingsRequest? Settings { get; set; }
    }
}
