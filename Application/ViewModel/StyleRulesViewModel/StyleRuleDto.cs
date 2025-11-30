using Application.ViewModel.ElementsViewModel;
using Domain.Entities;

namespace Application.ViewModel.StyleRulesViewModel
{
    public class StyleRuleDto
    {
        public Guid StyleRuleId { get; set; }
        public string Selector { get; set; } = string.Empty;
        public Dictionary<string, string>? Rules { get; set; }
        public StyleRuleType RuleType { get; set; } = StyleRuleType.Custom;
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ElementDto? ElementDto { get; set; }
    }
}
