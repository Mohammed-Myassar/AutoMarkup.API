using Application.ViewModel.GenerationRequest;

namespace Application.Abstractions
{
    public interface IBuildeCode
    {
        Task<(string GeneratedHtml, string GeneratedCss)>
            GenerateAsync(GenerateCode root, bool includeDefaultCss = true);

        Task<string> BuildHtmlAsync(GenerateCode root);

        Task<string> BuildCssAsync(GenerateCode root);
    }
}
