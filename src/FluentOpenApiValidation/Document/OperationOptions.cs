namespace FluentOpenApiValidation
{
    public record OperationOptions
    {
        public IReadOnlyCollection<Parameter> Parameters { get; set; } = Array.Empty<Parameter>();
        public Security Security { get; set; } = default;
    }
}