namespace Application.Helpers.DefaultRepository
{
    public static class DefaultCssRepository
    {
        public static readonly Dictionary<string, string> Defaults = new()
        {
            ["resetMargins"] = @"/* resetMargins */ 
html, body {
    margin: 0;
    padding: 0; 
}
",

            ["baseTypography"] = @"/* baseTypography */
body {
    font-family: Arial, Helvetica, sans-serif;
    line-height:1.4;
    color:#222;
}",

            ["containerDefaults"] = @"/* containerDefaults */
.container { 
    max-width: 1200px;
    margin: 0 auto;
    padding: 16px;
}",

            ["imagesResponsive"] = @"/* imagesResponsive */
img { max-width: 100%;
    height: auto;
    display:block;
}"
        };
    }
}
