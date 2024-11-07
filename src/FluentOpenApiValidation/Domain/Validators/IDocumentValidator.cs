namespace FluentOpenApiValidation
{
    internal interface IDocumentValidator
    {
        void Validate(ValidationContext context);
    }
}