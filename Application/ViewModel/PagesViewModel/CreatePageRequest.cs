namespace Application.ViewModel.PagesViewModel
{
    public class CreatePageRequest
    {
        public required string PageName { get; set; }
        public required Guid ProjectId { get; set; }
    }
}
