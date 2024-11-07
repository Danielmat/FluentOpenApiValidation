namespace FluentOpenApiValidation
{
    internal abstract class BaseValidator : IDocumentValidator
    {
        protected static class JsonPaths
        {
            public const string Paths = "paths";
            public const string Components = "components";
            public const string Schemas = "schemas";
            public const string RequestBody = "requestBody";
            public const string Content = "content";
            public const string Parameters = "parameters";
            public const string Responses = "responses";
            public const string Name = "name";
            public const string Type = "type";
            public const string Format = "format";
            public const string Schema = "schema";
            public const string Required = "required";
            public const string In = "in";
            public const string SecuritySchemes = "securitySchemes";
        }

        public void Validate(ValidationContext context) => ValidateInternal(context);

        protected abstract void ValidateInternal(ValidationContext context);
    }
}