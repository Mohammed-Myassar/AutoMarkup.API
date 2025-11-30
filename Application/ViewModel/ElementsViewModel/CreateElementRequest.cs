using Application.ViewModel.StyleRulesViewModel;

namespace Application.ViewModel.ElementsViewModel
{
    public class CreateElementRequest
    {
        public required string Type { get; set; }
        public required string InnerText { get; set; }
        public Dictionary<string, string>? Attributes { get; set; }
        public string? CssClasses { get; set; }
        public required Guid PageId { get; set; }
        public List<CreateElementRequest>? Children { get; set; }
        public List<StyleRuleDto>? StyleRules { get; set; }
        public int Order { get; set; } = 0;
    }
}
