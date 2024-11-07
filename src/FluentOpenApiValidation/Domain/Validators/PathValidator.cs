namespace FluentOpenApiValidation
{
    internal sealed class PathValidator : BaseValidator
    {
        protected override void ValidateInternal(ValidationContext context)
        {
            if (!context.Document.Element.TryGetPath(JsonPaths.Paths, out _))
            {
                context.AddError("INVALID_PATH", $"Path '{context.Document.Path}' not found");
            }
        }
    }
}