namespace FluentOpenApiValidation
{
    internal sealed class DocumentValidatorBuilder
    {
        private readonly List<IDocumentValidator> _validators = [];

        public DocumentValidatorBuilder AddValidationStep<TValidator>() where TValidator : IDocumentValidator, new()
        {
            _validators.Add(new TValidator());
            return this;
        }

        public IDocumentValidator Build() => new CompositeValidator(_validators);
    }
}