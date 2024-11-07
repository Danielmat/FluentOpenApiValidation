using System.Text.Json;

namespace FluentOpenApiValidation
{
    internal sealed class SchemaValidator : BaseValidator
    {
        protected override void ValidateInternal(ValidationContext context)
        {
            if (context.Document.Schema is not null)
            {
                var schema = GetSchemaElement(context);
                if (schema is null)
                {
                    context.AddError(
                      "INVALID_SCHEMA",
                      $"Schema '{context.Document.Schema.Name}' not found");

                    return;
                }

                ValidateProperties(context, context.Document.Schema.Properties, schema.Value);
            }
        }

        private static JsonElement? GetSchemaElement(ValidationContext context) =>
            context.Document.Element.TryGetPath(
                $"{JsonPaths.Components}.{JsonPaths.Schemas}.{context.Document.Schema.Name}",
                out var element)
                ? element
                : null;

        private static void ValidateProperties(ValidationContext context, IEnumerable<Property> properties, JsonElement schema)
        {
            if (properties is null) return;

            foreach (var property in properties)
            {
                ValidateProperty(context, property, schema);
            }
        }

        private static void ValidateProperty(ValidationContext context, Property property, JsonElement schema)
        {
            if (!schema.TryGetProperty("properties", out var propertiesElement) ||
                !propertiesElement.TryGetProperty(property.Name, out var propertyElement))
            {
                context.AddError(
                    "INVALID_PROPERTY",
                    $"Property '{property.Name}' not found in schema");
                return;
            }

            ValidatePropertyType(context, property, propertyElement);
            ValidatePropertyFormat(context, property, propertyElement);
        }

        private static void ValidatePropertyType(ValidationContext context, Property property, JsonElement element)
        {
            if (!property.Type.HasValue) return;

            if (!element.TryGetProperty(JsonPaths.Type, out var typeElement) || typeElement.GetString() != property.Type.Value.ToDescriptionString())
            {
                context.AddError(
                    "INVALID_PROPERTY_TYPE",
                    $"Invalid type for property '{property.Name}'");
            }
        }

        private static void ValidatePropertyFormat(ValidationContext context, Property property, JsonElement element)
        {
            if (!property.Format.HasValue) return;

            if (!element.TryGetProperty(JsonPaths.Format, out var formatElement) || formatElement.GetString() != property.Format.Value.ToDescriptionString())
            {
                context.AddError(
                    "INVALID_PROPERTY_FORMAT",
                    $"Invalid format for property '{property.Name}'");
            }
        }
    }
}