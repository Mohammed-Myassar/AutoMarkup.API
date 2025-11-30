using Application.Abstractions;
using Application.ViewModel.ElementsViewModel;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interface;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ElementService : IElementService
    {
        private readonly IPageRepository pageRepository;
        private readonly IElementRepository elementRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ElementService> logger;

        public ElementService(
            IPageRepository pageRepository,
            IElementRepository elementRepository,
            IMapper mapper,
            ILogger<ElementService> logger)
        {
            this.pageRepository = pageRepository;
            this.elementRepository = elementRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<List<ElementDto>> AddElementsBatchAsync(Guid pageId,
            List<CreateElementRequest> request)
        {
            var page = await pageRepository.GetByIdAsync(pageId);
            if (page == null)
                throw new ArgumentException("Page not found");

            var elements = mapper.Map<List<Element>>(request);
            var createdElements = new List<Element>();

            foreach (var element in elements)
            {
                element.PageId = pageId;
                var createdElement = await elementRepository.AddAsync(element);
                createdElements.Add(createdElement);
            }

            logger.LogInformation("Added {Count} elements to page {PageId}", elements.Count, pageId);
            return mapper.Map<List<ElementDto>>(createdElements);
        }

        public async Task<List<ElementDto>> UpdateElementsBatchAsync(Guid pageId, List<UpdateElementRequest> request)
        {
            var page = await pageRepository.GetByIdAsync(pageId);
            if (page == null) throw new ArgumentException("Page not found");

            var updatedElements = new List<ElementDto>();

            foreach (var update in request)
            {
                var element = await elementRepository
                    .GetByIdIncludingAsync(update.ElementId, e => e.StyleRules!);

                if (element != null)
                {
                    if (element.PageId != pageId)
                    {
                        logger.LogWarning("Element {ElementId} does not belong to page {PageId}", update.ElementId, pageId);
                        throw new InvalidOperationException("Element does not belong to this page");
                    }

                    mapper.Map(update, element);
                    await elementRepository.UpdateAsync(element);
                    updatedElements.Add(mapper.Map<ElementDto>(element));
                }
                else
                {
                    logger.LogWarning("Element not found: {ElementId}", update.ElementId);
                    throw new ArgumentException($"Element not found: {update.ElementId}");
                }
            }

            logger.LogInformation("Updated {Count} elements in page {PageId}", updatedElements.Count, pageId);
            return updatedElements;
        }
    }
}
