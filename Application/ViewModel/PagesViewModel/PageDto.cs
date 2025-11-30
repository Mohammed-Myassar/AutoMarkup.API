namespace Application.ViewModel.PagesViewModel
{
    public class PageDto
    {
        public Guid PageId { get; set; }
        public string PageName { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public int ElementsCount { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
