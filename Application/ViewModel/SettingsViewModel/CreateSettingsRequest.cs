using Domain.Entities;

namespace Application.ViewModel.SettingsViewModel
{
    public class CreateSettingsRequest
    {
        public bool IncludeDefaultCss { get; set; } = true;
        public bool ResetCss { get; set; } = false;
        public string? Font { get; set; }
        public ProjectType ProjectType { get; set; } = ProjectType.Normal;
        public string? BootstrapVersion { get; set; }
        public bool IncludeBootstrapIcons { get; set; } = true;
        public bool IncludeBootstrapJS { get; set; } = true;
    }
}
