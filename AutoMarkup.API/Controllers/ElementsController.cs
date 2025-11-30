using Application.Abstractions;
using Application.ViewModel.ElementsViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarkup.API.Controllers
{
    [ApiController]
    [Route("api/pages/{pageId:guid}/[controller]")]
    public class ElementsController : ControllerBase
    {
        private readonly IElementService elementService;
        private readonly ILogger<ElementsController> logger;

        public ElementsController(
            IElementService elementService,
            ILogger<ElementsController> logger
            )
        {
            this.elementService = elementService;
            this.logger = logger;
        }

        [HttpPost("add-elements")]
        public async Task<ActionResult<List<ElementDto>>> AddElementsBatch(
            Guid pageId,
            [FromBody] List<CreateElementRequest> request)
        {
            try
            {
                var elements = await elementService
                    .AddElementsBatchAsync(pageId, request);
                return Ok(elements);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-elements")]
        public async Task<ActionResult<List<ElementDto>>> UpdateElementsBatch(
            Guid pageId,
            [FromBody] List<UpdateElementRequest> request)
        {
            try
            {
                var elements = await elementService
                    .UpdateElementsBatchAsync(pageId, request);
                return Ok(elements);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
