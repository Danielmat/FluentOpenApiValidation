namespace FluentOpenApiValidation
{
    public record PostOperationOptions
    {
        public required Request Request { get; set; }
        public Security Security { get; set; }
    }
}