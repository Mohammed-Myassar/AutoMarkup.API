namespace Application.ViewModel.StyleRulesViewModel
{
    public class ApplyStyleRequest
    {
        public required Guid ElementId { get; set; }
        public required Guid StyleRuleId { get; set; }
        public int Order { get; set; } = 0;
    }
}
