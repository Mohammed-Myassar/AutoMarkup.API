using Domain.Entities;

namespace Application.ViewModel.SettingsViewModel
{
    public class UpdateSettingsRequest
    {
        public bool? IncludeDefaultCss { get; set; }
        public bool? ResetCss { get; set; }
        public string? Font { get; set; }
        public ProjectType? ProjectType { get; set; }
        public string? BootstrapVersion { get; set; }
        public bool? IncludeBootstrapIcons { get; set; }
        public bool? IncludeBootstrapJS { get; set; }
    }
}
