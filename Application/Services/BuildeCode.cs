using Application.Helpers;
using Application.Helpers.DefaultRepository;
using Application.Abstractions;
using Application.ViewModel.GenerationRequest;
using Scriban;
using System.Text;

namespace Application.Services
{
    public class BuildeCode : IBuildeCode
    {
        public string GeneratedHtml { get; private set; } = string.Empty;
        public string GeneratedCss { get; private set; } = string.Empty;

        public async Task<(string GeneratedHtml, string GeneratedCss)>
            GenerateAsync(GenerateCode root, bool includeDefaultCss = true)
        {
            var cssBuilder = new StringBuilder();

            if (includeDefaultCss)
            {
                foreach (var kv in DefaultCssRepository.Defaults)
                {
                    cssBuilder.AppendLine(kv.Value);
                }
            }

            var htmlTask = BuildHtmlAsync(root);
            var cssTask = BuildCssAsync(root);

            await Task.WhenAll(htmlTask, cssTask);

            var htmlResult = htmlTask.Result;
            var cssResult = cssTask.Result;

            GeneratedHtml = $"""
                {DefaultHtmlRepository.DefaultOpenPgeHtml}
                {htmlResult}
                {DefaultHtmlRepository.DefaultClosePgeHtml}
                """;

            GeneratedCss = cssBuilder.AppendLine(cssResult).ToString();

            return (GeneratedHtml, GeneratedCss);
        }

        public async Task<string> BuildHtmlAsync(GenerateCode root)
        {
            try
            {
                string templateString = HelpersGenerator.BuildTemplateString(root);
                var template = Template.Parse(templateString);

                var model = await BuildTemplateModelAsync(root);
                return (await template.RenderAsync(model)).Trim();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error building HTML for element {root.Type}", ex);
            }
        }

        public async Task<string> BuildCssAsync(GenerateCode root)
        {
            var sb = new StringBuilder();
            await GenerateCssRecursiveAsync(root, sb);
            return sb.ToString().Trim();
        }

        public async Task<object> BuildTemplateModelAsync(GenerateCode root)
        {
            if (HelpersGenerator.IsSelfClosingTag(root.Type!))
            {
                return new { };
            }

            var childrenContent = new List<string>();

            if (HelpersGenerator.HasChildren(root))
            {
                foreach (var child in root.Children!)
                {
                    childrenContent.Add(await BuildHtmlAsync(child));
                }
            }

            return new
            {
                children = childrenContent,
                inner_text = root.InnerText
            };
        }

        public async Task GenerateCssRecursiveAsync(GenerateCode element, StringBuilder sb)
        {
            if (element.Props != null && element.Props.Any())
            {
                string selector = HelpersGenerator.GenerateSelector(element);
                string styles = HelpersGenerator.ExtractStyles(element.Props);

                if (!string.IsNullOrEmpty(styles))
                    sb.AppendLine($"{selector} {{ {styles} }}");
            }

            if (element.Children != null)
            {
                foreach (var child in element.Children)
                {
                    await GenerateCssRecursiveAsync(child, sb);
                }
            }
        }
    }
}