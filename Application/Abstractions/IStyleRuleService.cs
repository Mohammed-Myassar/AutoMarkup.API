using Application.ViewModel.StyleRulesViewModel;

namespace Application.Abstractions
{
    public interface IStyleRuleService
    {
        Task<List<StyleRuleDto>>
            CreateStyleRulesBatchAsync(List<CreateStyleRuleRequest> requests);

        Task<List<StyleRuleDto>>
            UpdateStyleRulesBatchAsync(List<UpdateStyleRuleRequest> requests);
    }
}
