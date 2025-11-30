using Domain.AccountEntity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Project
    {
        public Project()
        {
            ProjectId = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        [Key]
        public Guid ProjectId { get; set; }

        [Required, MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public required string Description { get; set; }

        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }

        public required Guid UserId { get; set; }
        public User? User { get; set; }

        public IEnumerable<Page>? Pages { get; set; }
        public Settings? Settings { get; set; }
        public IEnumerable<StyleRule>? StyleRules { get; set; }
    }
}
