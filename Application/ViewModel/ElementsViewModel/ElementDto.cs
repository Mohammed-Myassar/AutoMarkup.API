using Application.ViewModel.StyleRulesViewModel;

namespace Application.ViewModel.ElementsViewModel
{
    public class ElementDto
    {
        public Guid ElementId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string InnerText { get; set; } = string.Empty;
        public Dictionary<string, string>? Attributes { get; set; }
        public string? CssClasses { get; set; }
        public Guid PageId { get; set; }
        public int Order { get; set; }

        public List<ElementDto>? Children { get; set; }
        public List<StyleRuleDto>? StyleRules { get; set; }
    }
}

