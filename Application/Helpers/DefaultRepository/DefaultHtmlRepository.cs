namespace Application.Helpers.DefaultRepository
{
    public static class DefaultHtmlRepository
    {
        public static readonly string DefaultOpenPgeHtml = """
        <!DOCTYPE html>
        <html lang="en">
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width", initial-scale="1.0">
            <title>{{PageTitle}}</title>
        </head>
        <body>
        """;

        public static readonly string DefaultClosePgeHtml = "</body>\r\n</html>";
    }
}
