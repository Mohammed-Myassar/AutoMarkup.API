using Application.ViewModel.GenerationRequest;

namespace Application.Helpers
{
    public static class HelpersGenerator
    {
        private static readonly HashSet<string> _selfClosingTags = new()
            {
            "img", "input", "br", "hr", "meta", "link", "area", "base",
            "col", "embed", "source", "track", "wbr"
            };

        // ======= Html Helper Tools =======
        public static string BuildTemplateString(GenerateCode root)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root), "Not must be root is null");

            var attributes = BuildAttributes(root);
            var attributesString = attributes.Any() ? " " + string.Join(" ", attributes) : "";

            if (IsSelfClosingTag(root.Type!))
            {
                return $"<{root.Type}{attributesString} />";
            }

            if (HasChildren(root))
            {
                var childrenHtml = string.Join("\n", root.Children!.Select(child =>
                    BuildTemplateString(child)
                ));

                return $@"<{root.Type}{attributesString}>
                    {childrenHtml}
                    </{root.Type}>";
            }
            else if (!string.IsNullOrEmpty(root.InnerText))
            {
                return $@"<{root.Type}{attributesString}>{root.InnerText}</{root.Type}>";
            }
            else
            {
                return $@"<{root.Type}{attributesString}></{root.Type}>";
            }
        }

        public static bool IsSelfClosingTag(string tagType)
        {
            if (string.IsNullOrEmpty(tagType))
                return false;

            return _selfClosingTags.Contains(tagType.ToLower());
        }

        public static List<string> BuildAttributes(GenerateCode element)
        {
            var attributes = new List<string>();

            if (!string.IsNullOrEmpty(element.Id))
                attributes.Add($"id='{element.Id!.Replace(" ", "-")}'");

            if (!string.IsNullOrEmpty(element.ClassName))
                attributes.Add($"class='{element.ClassName!.Replace(" ", "-")}'");

            if (!string.IsNullOrEmpty(element.Src))
                attributes.Add($"src='{element.Src}'");

            if (!string.IsNullOrEmpty(element.Alt))
                attributes.Add($"alt='{element.Alt}'");

            if (!string.IsNullOrEmpty(element.Href))
                attributes.Add($"href='{element.Href}'");

            return attributes;
        }

        public static bool HasChildren(GenerateCode element)
        {
            return element.Children != null && element.Children.Count > 0;
        }

        // ======= Css Helper Tools =======
        public static string GenerateSelector(GenerateCode element)
        {
            if (!string.IsNullOrEmpty(element.Id))
                return $"#{element.Id.Replace(" ", "-")}";

            else if (!string.IsNullOrEmpty(element.ClassName))
                return $".{element.ClassName.Replace(" ", "-")}";

            return element.Type!;
        }

        public static string ExtractStyles(Dictionary<string, string> props)
        {
            var styles = new List<string>();

            if (props != null)
            {
                foreach (var prop in props)
                {
                    if (!string.IsNullOrEmpty(prop.Value))
                    {
                        styles.Add($"{prop.Key}: {prop.Value}");
                    }
                }
            }

            return string.Join("; ", styles);
        }
    }
}