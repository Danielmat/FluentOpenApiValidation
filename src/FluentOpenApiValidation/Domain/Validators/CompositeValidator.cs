namespace FluentOpenApiValidation
{
    internal sealed class CompositeValidator(IEnumerable<IDocumentValidator> validators) : IDocumentValidator
    {
        public void Validate(ValidationContext context) =>
            validators.ForEach(validator => validator.Validate(context));
    }
}