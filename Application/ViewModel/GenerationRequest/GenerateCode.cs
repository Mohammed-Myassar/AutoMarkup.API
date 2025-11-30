namespace Application.ViewModel.GenerationRequest
{
    public class GenerateCode
    {
        public string? Type { get; set; }
        public string? InnerText { get; set; }
        public Dictionary<string, string>? Props { get; set; }
        public List<GenerateCode>? Children { get; set; }
        public string? Id { get; set; }
        public string? ClassName { get; set; }
        public string? Src { get; set; }
        public string? Alt { get; set; }
        public string? Href { get; set; }
    }
}
