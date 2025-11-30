using Application.ViewModel.PagesViewModel;

namespace Application.Abstractions
{
    public interface IPageService
    {
        Task<PageDto> AddPageAsync(Guid projectId, CreatePageRequest request);
        Task<List<PageDto>> GetProjectPagesAsync(Guid projectId);
        Task<PageDetailsDto> GetPageDetailsAsync(Guid pageId);
        Task<PageDto> UpdatePageAsync(Guid pageId, UpdatePageRequest request);
        Task<bool> DeletePageAsync(Guid pageId);
    }
}
