using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Page
    {
        public Page()
        {
            PageId = Guid.NewGuid();
        }

        [Key]
        public Guid PageId { get; set; }

        [Required, MaxLength(100)]
        public required string PageName { get; set; }

        public required Guid ProjectId { get; set; }
        public Project? Project { get; set; }

        public IEnumerable<Element>? Elements { get; set; }
    }
}
