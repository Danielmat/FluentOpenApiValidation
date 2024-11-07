using System.Text.Json;

namespace FluentOpenApiValidation
{
    internal sealed class SecuritySchemaValidator : BaseValidator
    {
        protected override void ValidateInternal(ValidationContext context)
        {
            if (context.Document.SecuritySchema.HasValue)
            {
                if (GetSecuritySchemaElement(context) is null)
                {
                    context.AddError(
                      "INVALID_SECURITY_SCHEMA",
                      $"Schema '{context.Document.SecuritySchema.Value.Name}' not found");
                }
            }
        }

        private static JsonElement? GetSecuritySchemaElement(ValidationContext context) =>
          context.Document.Element.TryGetPath(
              $"{JsonPaths.Components}.{JsonPaths.SecuritySchemes}.{context.Document.SecuritySchema.Value.Name}",
              out var element)
              ? element
              : null;
    }
}