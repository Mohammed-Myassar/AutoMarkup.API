using Application.Abstractions;
using Application.ViewModel.PagesViewModel;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interface;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository pageRepository;
        private readonly IProjectRepository projectRepository;
        private readonly IMapper mapper;
        private readonly ILogger<PageService> logger;

        public PageService(
            IPageRepository pageRepository,
            IProjectRepository projectRepository,
            IMapper mapper,
            ILogger<PageService> logger)
        {
            this.pageRepository = pageRepository;
            this.projectRepository = projectRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<PageDto> AddPageAsync(Guid projectId, CreatePageRequest request)
        {
            var project = await projectRepository.GetByIdAsync(projectId);

            if (project == null)
                throw new ArgumentException("Project not found");

            var page = mapper.Map<Page>(request);
            var createdPage = await pageRepository.AddAsync(page);

            logger.LogInformation($"Page added: {page.PageName} to project {request.ProjectId}");

            return mapper.Map<PageDto>(createdPage);
        }

        public async Task<List<PageDto>> GetProjectPagesAsync(Guid projectId)
        {
            var pages = await pageRepository.FindAsync(p => p.ProjectId == projectId);
            return mapper.Map<List<PageDto>>(pages);
        }

        public async Task<PageDetailsDto> GetPageDetailsAsync(Guid pageId)
        {
            var page = await pageRepository.GetByIdIncludingAsync(pageId, p => p.Elements!);

            if (page == null)
                throw new ArgumentException("Page not found");

            return mapper.Map<PageDetailsDto>(page);
        }

        public async Task<PageDto> UpdatePageAsync(Guid pageId, UpdatePageRequest request)
        {
            var page = await pageRepository.GetByIdAsync(pageId);

            if (page == null)
                throw new ArgumentException("Page not found");

            mapper.Map(request, page);
            var updatedPage = await pageRepository.UpdateAsync(page);

            logger.LogInformation($"Page updated: {pageId}");
            return mapper.Map<PageDto>(updatedPage);
        }

        public async Task<bool> DeletePageAsync(Guid pageId)
        {
            var page = await pageRepository.GetByIdAsync(pageId);

            if (page == null)
                throw new ArgumentException("Page not found");

            await pageRepository.DeleteAsync(page);
            logger.LogInformation($"Page deleted: {pageId}");
            return true;
        }
    }
}
