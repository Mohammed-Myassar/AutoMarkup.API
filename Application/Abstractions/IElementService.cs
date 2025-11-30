using Application.ViewModel.ElementsViewModel;

namespace Application.Abstractions
{
    public interface IElementService
    {
        Task<List<ElementDto>> AddElementsBatchAsync(Guid pageId,
            List<CreateElementRequest> request);
        Task<List<ElementDto>> UpdateElementsBatchAsync(Guid pageId,
            List<UpdateElementRequest> request);
    }
}
