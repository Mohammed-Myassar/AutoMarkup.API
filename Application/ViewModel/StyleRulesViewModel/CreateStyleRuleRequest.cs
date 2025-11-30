using Domain.Entities;

namespace Application.ViewModel.StyleRulesViewModel
{
    public class CreateStyleRuleRequest
    {
        public required string Selector { get; set; }
        public required Dictionary<string, string> Rules { get; set; }
        public StyleRuleType RuleType { get; set; } = StyleRuleType.Custom;

        public required Guid ProjectId { get; set; }
        public required Guid? ElementId { get; set; }
    }
}
