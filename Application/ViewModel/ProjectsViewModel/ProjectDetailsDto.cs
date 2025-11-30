using Application.ViewModel.PagesViewModel;
using Application.ViewModel.SettingsViewModel;
using Application.ViewModel.StyleRulesViewModel;
using Application.ViewModel.UsersViewModel;

namespace Application.ViewModel.ProjectsViewModel
{
    public class ProjectDetailsDto
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserDto? User { get; set; }
        public SettingsDto? Settings { get; set; }
        public List<PageDto>? Pages { get; set; }
        public List<StyleRuleDto>? StyleRules { get; set; }
    }
}
