using Application.Abstractions;
using Application.ViewModel.PagesViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarkup.API.Controllers
{
    [ApiController]
    [Route("api/projects/{projectId:guid}/[controller]")]
    public class PagesController : ControllerBase
    {
        private readonly IPageService pageService;
        private readonly ILogger<PagesController> logger;

        public PagesController(
            IPageService pageService,
            ILogger<PagesController> logger
            )
        {
            this.pageService = pageService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<PageDto>> AddPage(
            Guid projectId, [FromBody] CreatePageRequest request)
        {
            try
            {
                var page = await pageService.AddPageAsync(projectId, request);
                return Ok(page);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<PageDto>>> GetProjectPages(Guid projectId)
        {
            var pages = await pageService.GetProjectPagesAsync(projectId);
            return Ok(pages);
        }

        [HttpGet("{pageId:guid}")]
        public async Task<ActionResult<PageDetailsDto>> GetPage(Guid projectId, Guid pageId)
        {
            try
            {
                var page = await pageService.GetPageDetailsAsync(pageId);
                return Ok(page);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{pageId:guid}")]
        public async Task<ActionResult<PageDto>> UpdatePage(
            Guid projectId, Guid pageId, [FromBody] UpdatePageRequest request)
        {
            try
            {
                var page = await pageService.UpdatePageAsync(pageId, request);
                return Ok(page);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{pageId:guid}")]
        public async Task<ActionResult> DeletePage(Guid projectId, Guid pageId)
        {
            try
            {
                await pageService.DeletePageAsync(pageId);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}
