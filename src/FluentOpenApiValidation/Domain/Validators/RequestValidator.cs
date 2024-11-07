namespace FluentOpenApiValidation
{
    internal sealed class RequestValidator : BaseValidator
    {
        protected override void ValidateInternal(ValidationContext context)
        {
            if (context.Document.Method == HttpMethod.Post)
            {
                if (!ValidateRequestPath(context.Document, context.Document.Request, out var errorCode))
                {
                    context.AddError(errorCode, nameof(RequestValidator));
                }
            }
        }

        private static bool ValidateRequestPath(IDocument document, Request request, out string errorCode)
        {
            var pathPattern =
                $"{JsonPaths.Paths}.{document.Path}.{document.Method.Method.ToLower()}.{JsonPaths.RequestBody}.{JsonPaths.Content}.{request.MediaTypes.ToDescriptionString()}";

            bool isValid = document.Element.TryGetPath(pathPattern, out _);

            errorCode = isValid ? string.Empty : $"INVALID_REQUEST_VALIDATION {pathPattern}";

            return isValid;
        }
    }
}