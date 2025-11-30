using Application.ViewModel.GenerationRequest;

namespace Application.Abstractions
{
    public interface ICodeGenerationService
    {
        Task<(string Html, string Css)>
            GeneratedFilesForAllPagesAsync(GenerateCode request);
    }
}
