using Application.Abstractions;
using Application.ViewModel.GenerationRequest;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarkup.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportsController : ControllerBase
    {
        private readonly ICodeGenerationService generationService;
        private readonly IFileDownloadService downloadService;

        public ExportsController(ICodeGenerationService generationService,
            IFileDownloadService downloadService
            )
        {
            this.generationService = generationService;
            this.downloadService = downloadService;
        }

        [HttpPost("download-zip")]
        public async Task<IActionResult> DownloadZip([FromBody] GenerateCode request)
        {
            try
            {
                var files = new Dictionary<string, string>();

                var results = await generationService.GeneratedFilesForAllPagesAsync(request);
                
                    files[$"pages/loj/index.html"] = results.Html;
                    files[$"pages/uhuo/style.css"] = results.Css;
     

                var projectName = "Project";

                files["README.txt"] = $@"
Project: {projectName}
Number of pages: {0}
Created: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}
";

                var zipPath = downloadService.DownloadZipToDownloads(files, projectName);

                return Ok(new { ZipPath = zipPath, PagesCount = 2 });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
