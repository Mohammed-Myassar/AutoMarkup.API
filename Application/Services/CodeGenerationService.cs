using Application.Abstractions;
using Application.ViewModel.GenerationRequest;
using AutoMapper;

namespace Application.Services
{
    public class CodeGenerationService : ICodeGenerationService
    {
        private readonly IBuildeCode buildeCode;
        private readonly IMapper mapper;

        public CodeGenerationService(
            IBuildeCode buildeCode,
            IMapper mapper
            )
        {
            this.buildeCode = buildeCode;
            this.mapper = mapper;
        }

        public async Task<(string Html, string Css)>
            GeneratedFilesForAllPagesAsync(GenerateCode rootElement)
        {
            return await buildeCode.GenerateAsync(rootElement, true);
        }
    }
}
