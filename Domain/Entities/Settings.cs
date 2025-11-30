using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Settings
    {
        public Settings()
        {
            SettingsId = Guid.NewGuid();
        }

        [Key]
        public Guid SettingsId { get; set; }

        public bool IncludeDefaultCss { get; set; } = true;
        public bool ResetCss { get; set; } = false;

        [MaxLength(50)]
        public string Font { get; set; } = "Arial";

        public ProjectType ProjectType { get; set; } = ProjectType.Normal;

        public string BootstrapVersion { get; set; } = "5.3.0";
        public bool IncludeBootstrapIcons { get; set; } = true;
        public bool IncludeBootstrapJS { get; set; } = true;

        public required Guid ProjectId { get; set; }
        public Project? Project { get; set; }
    }

    public enum ProjectType : byte
    {
        Normal = 0,
        Bootstrap = 1,
        Tailwind = 2
    }
}
