using Application.ViewModel.ElementsViewModel;
using Application.ViewModel.ProjectsViewModel;
using Application.ViewModel.StyleRulesViewModel;

namespace Application.ViewModel.PagesViewModel
{
    public class PageDetailsDto
    {
        public Guid PageId { get; set; }
        public string PageName { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public ProjectDto? Project { get; set; }
        public List<ElementDto>? Elements { get; set; }
        public List<StyleRuleDto>? StyleRules { get; set; }
    }
}
