using Application.Abstractions;
using Application.ViewModel.StyleRulesViewModel;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interface;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class StyleRuleService : IStyleRuleService
    {
        private readonly IElementRepository elementRepository;
        private readonly IStyleRuleRepository styleRuleRepository;
        private readonly IMapper mapper;
        private readonly ILogger<StyleRuleService> logger;

        public StyleRuleService(
            IElementRepository elementRepository,
            IStyleRuleRepository styleRuleRepository,
            IMapper mapper,
            ILogger<StyleRuleService> logger)
        {
            this.elementRepository = elementRepository;
            this.styleRuleRepository = styleRuleRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<List<StyleRuleDto>>
            CreateStyleRulesBatchAsync(List<CreateStyleRuleRequest> requests)
        {
            var tasks = requests.Select(request => CreateStyleRuleAsync(request));
            var results = await Task.WhenAll(tasks);

            logger.LogInformation("Created {Count} style rules", results.Length);
            return results.ToList();
        }

        public async Task<List<StyleRuleDto>>
            UpdateStyleRulesBatchAsync(List<UpdateStyleRuleRequest> requests)
        {
            var tasks = requests.Select(request =>
                UpdateStyleRuleAsync(request.StyleRuleId, request));
            var results = await Task.WhenAll(tasks);

            logger.LogInformation("Updated {Count} style rules", results.Length);
            return results.ToList();
        }

        public async Task<StyleRuleDto> CreateStyleRuleAsync(CreateStyleRuleRequest request)
        {
            if (request.ElementId.HasValue)
            {
                var element = await elementRepository.GetByIdAsync(request.ElementId.Value);
                if (element == null)
                    throw new ArgumentException($"Element with ID {request.ElementId} not found");
            }

            var styleRule = mapper.Map<StyleRule>(request);
            var createdStyleRule = await styleRuleRepository.AddAsync(styleRule);

            logger.LogInformation("Created style rule {StyleRuleId}", createdStyleRule.StyleRuleId);
            return mapper.Map<StyleRuleDto>(createdStyleRule);
        }

        public async Task<StyleRuleDto> UpdateStyleRuleAsync(Guid styleRuleId,
            UpdateStyleRuleRequest request)
        {
            var styleRule = await styleRuleRepository.GetByIdAsync(styleRuleId);
            if (styleRule == null)
                throw new ArgumentException($"Style rule with ID {styleRuleId} not found");

            if (request.ElementId.HasValue)
            {
                var element = await elementRepository.GetByIdAsync(request.ElementId.Value);
                if (element == null)
                    throw new ArgumentException($"Element with ID {request.ElementId} not found");
                styleRule.ElementId = request.ElementId.Value;
            }

            mapper.Map(request, styleRule);
            await styleRuleRepository.UpdateAsync(styleRule);

            logger.LogInformation("Updated style rule {StyleRuleId}", styleRuleId);
            return mapper.Map<StyleRuleDto>(styleRule);
        }

        public async Task<bool> DeleteStyleRuleAsync(Guid styleRuleId)
        {
            var styleRule = await styleRuleRepository.GetByIdAsync(styleRuleId);
            if (styleRule == null)
                throw new ArgumentException($"Style rule with ID {styleRuleId} not found");

            await styleRuleRepository.DeleteAsync(styleRule);
            logger.LogInformation("Deleted style rule {StyleRuleId}", styleRuleId);
            return true;
        }
    }
}