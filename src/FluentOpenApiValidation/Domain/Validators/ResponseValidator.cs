namespace FluentOpenApiValidation
{
    internal sealed class ResponseValidator : BaseValidator
    {
        protected override void ValidateInternal(ValidationContext context)
        {
            if (!Validate(context.Document, context.Document.Responses, out var errorCode))
            {
                context.AddError(errorCode, nameof(ResponseValidator));
            }
        }

        private static bool Validate(IDocument document, IReadOnlyCollection<Response> responses, out string errorCode)
        {
            var validations = new List<(Func<bool> Check, string ErrorMessage)>();

            foreach (var response in responses)
            {
                var pathPattern = $"{JsonPaths.Paths}.{document.Path}.{document.Method.Method.ToLower()}.{JsonPaths.Responses}.{(int)response.HttpStatusCode}";

                validations.Add((() => document.Element.TryGetPath(pathPattern, out _),
                        $"INVALID_REPONSE_STATUS_CODE_CONFIGURATION {response.HttpStatusCode.ToDescriptionString()}"));

                validations.Add((() => !response.SchemaType.HasValue || response.MediaTypes.HasValue,
                    "INVALID_MEDIA_TYPE_CONFIGURATION"));

                if (response.MediaTypes.HasValue)
                {
                    pathPattern += $".{JsonPaths.Content}.{response.MediaTypes.ToDescriptionString()}";

                    validations.Add((() => document.Element.TryGetPath(pathPattern, out _),
                    $"INVALID_RESPONSE_VALIDATION {pathPattern}"));
                }

                if (response.SchemaType.HasValue)
                {
                    validations.Add((() =>
                    {
                        return document.Element.TryGetPath($"{pathPattern}.{JsonPaths.Schema}.{JsonPaths.Type}", out var type)
                               && type.GetString().Equals(response.SchemaType.ToDescriptionString(), StringComparison.InvariantCultureIgnoreCase);
                    }, $"INVALID_RESPONSE_VALIDATION {pathPattern}"));
                }
            }

            foreach (var (check, error) in validations)
            {
                if (!check())
                {
                    errorCode = error;
                    return false;
                }
            }

            errorCode = string.Empty;
            return true;
        }
    }
}