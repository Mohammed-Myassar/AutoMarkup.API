using Application.ViewModel.StyleRulesViewModel;

namespace Application.ViewModel.ElementsViewModel
{
    public class UpdateElementRequest
    {
        public required Guid ElementId { get; set; }
        public string? Type { get; set; }
        public string? InnerText { get; set; }
        public Dictionary<string, string>? Attributes { get; set; }
        public string? CssClasses { get; set; }
        public List<UpdateElementRequest>? Children { get; set; }
        public List<StyleRuleDto>? StyleRules { get; set; }
        public int? Order { get; set; }
    }
}
