namespace Application.Abstractions
{
    public interface IFileDownloadService
    {
        string DownloadZipToDownloads(Dictionary<string, string> files, string projectName);
        string DownloadHtmlToDownloads(string htmlContent, string cssContent, string projectName);
    }
}
