using Domain.Entities;

namespace Application.ViewModel.SettingsViewModel
{
    public class SettingsDto
    {
        public Guid SettingsId { get; set; }
        public bool IncludeDefaultCss { get; set; }
        public bool ResetCss { get; set; }
        public string Font { get; set; } = "Arial";
        public ProjectType ProjectType { get; set; }
        public string BootstrapVersion { get; set; } = "5.3.0";
        public bool IncludeBootstrapIcons { get; set; }
        public bool IncludeBootstrapJS { get; set; }
        public Guid ProjectId { get; set; }
    }
}
