using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.AccountEntity
{
    public class User
    {
        public User()
        {
            UserId = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        [Key]
        public Guid UserId { get; set; }

        [Required, MaxLength(50)]
        public required string Username { get; set; }

        [Required, MaxLength(50)]
        public required string FirstName { get; set; }

        [Required, MaxLength(50)]
        public required string LastName { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; }

        public IEnumerable<Project>? Projects { get; set; }
    }
}
