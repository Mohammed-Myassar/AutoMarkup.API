using Application.Helpers;
using Application.Abstractions;
using System.IO.Compression;

namespace Application.Services
{
    public class FileDownloadService : IFileDownloadService
    {
        public string DownloadZipToDownloads(Dictionary<string, string> files,
                                            string projectName)
        {
            var downloadsPath = DownloadingHelper.GetDownloadsFolderPath();
            var zipFileName = $"{projectName}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.zip";
            var fullZipPath = Path.Combine(downloadsPath, zipFileName);

            using (var fileStream = new FileStream(fullZipPath, FileMode.Create))
            using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
            {
                foreach (var file in files)
                {
                    var entry = archive.CreateEntry(file.Key);
                    using var entryStream = entry.Open();
                    using var streamWriter = new StreamWriter(entryStream);
                    streamWriter.Write(file.Value);
                }
            }

            return fullZipPath;
        }

        public string DownloadHtmlToDownloads(string htmlContent, string cssContent,
                                string projectName)
        {
            var downloadsPath = DownloadingHelper.GetDownloadsFolderPath();
            var projectFolder = $"{projectName}_{DateTime.UtcNow:yyyyMMdd_HHmmss}";
            var fullProjectPath = Path.Combine(downloadsPath, projectFolder);

            Directory.CreateDirectory(fullProjectPath);

            var htmlPath = Path.Combine(fullProjectPath, "index.html");
            File.WriteAllText(htmlPath, htmlContent);

            var cssPath = Path.Combine(fullProjectPath, "styles.css");
            File.WriteAllText(cssPath, cssContent);

            var readmePath = Path.Combine(fullProjectPath, "README.txt");
            File.WriteAllText(readmePath, $"مشروع: {projectName}\nتم الإنشاء: {DateTime.UtcNow}");

            return fullProjectPath;
        }
    }
}
