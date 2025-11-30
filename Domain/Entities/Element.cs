using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Element
    {
        public Element()
        {
            ElementId = Guid.NewGuid();
        }

        [Key]
        public Guid ElementId { get; set; }

        [Required, MaxLength(50)]
        public required string Type { get; set; }

        public required string InnerText { get; set; }

        [NotMapped]
        public required Dictionary<string, string>? Attribute
        {
            get => Attributes?.ToDictionary(a => a.Key, a => a.Value);
            set => Attributes = value!.Select(v => new Attributes()
            {
                Key = v.Key,
                Value = v.Value
            }).ToList();
        }

        public string? CssClasses { get; set; }

        public required int Order { get; set; }

        public Guid PageId { get; set; }
        public Page? Page { get; set; }

        public Guid? ParentElementId { get; set; }
        public Element? ParentElement { get; set; }
        public IEnumerable<Element>? Children { get; set; }
        public IEnumerable<Attributes>? Attributes { get; set; }
        public IEnumerable<StyleRule>? StyleRules { get; set; }
    }
}
