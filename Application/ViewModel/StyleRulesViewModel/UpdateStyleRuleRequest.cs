using Domain.Entities;

namespace Application.ViewModel.StyleRulesViewModel
{
    public class UpdateStyleRuleRequest
    {
        public required Guid StyleRuleId { get; set; }
        public string? Selector { get; set; }
        public Dictionary<string, string>? Rules { get; set; }
        public StyleRuleType? RuleType { get; set; }

        public required Guid ProjectId { get; set; }
        public required Guid? ElementId { get; set; }
    }
}
