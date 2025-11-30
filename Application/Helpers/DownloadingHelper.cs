namespace Application.Helpers
{
    public static class DownloadingHelper
    {
        public static string GetDownloadsFolderPath()
        {
            return Environment
                .GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
        }
    }
}
