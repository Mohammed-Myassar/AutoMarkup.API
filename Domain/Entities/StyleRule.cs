using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public enum StyleRuleType : byte
    {
        Custom = 0,
        Bootstrap = 1,
        Component = 2
    }

    public class StyleRule
    {
        public StyleRule()
        {
            StyleRuleId = Guid.NewGuid();
        }

        [Key]
        public Guid StyleRuleId { get; set; }

        [Required, MaxLength(100)]
        public required string Selector { get; set; }

        [NotMapped]
        public required Dictionary<string, string>? Rule
        {
            get => Rules?.ToDictionary(a => a.Key, a => a.Value);
            set => Rules = value!.Select(kv => new Rule()
            {
                Key = kv.Key,
                Value = kv.Value
            }).ToList();
        }

        public StyleRuleType RuleType { get; set; } = StyleRuleType.Custom;

        public Guid ElementId { get; set; }
        public Element? Element { get; set; }

        public IEnumerable<Rule>? Rules { get; set; }
    }
}
