namespace Domain.Entities
{
    public class Rule
    {
        public Guid RuleId { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public Guid StyleRuleId { get; set; }
        public StyleRule? StyleRule { get; set; }
    }
}
