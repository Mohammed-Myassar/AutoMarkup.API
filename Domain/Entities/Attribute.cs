namespace Domain.Entities
{
    public class Attributes
    {
        public Guid AttributesId { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public Guid ElementId { get; set; }
        public Element? Element { get; set; }
    }
}
