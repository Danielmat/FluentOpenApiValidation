namespace FluentOpenApiValidation
{
    internal sealed class Contract : IContract
    {
        private readonly IDocumentValidator _validator;
        private readonly ValidationContext _context;

        public Contract(IDocument document)
        {
            ArgumentNullException.ThrowIfNull(document);

            _context = new(document);
            _validator = CreateValidators();
        }

        private IDocumentValidator CreateValidators()
            => new DocumentValidatorBuilder()
                .AddValidationStep<PathValidator>()
                .AddValidationStep<ParameterValidator>()
                .AddValidationStep<SecuritySchemaValidator>()
                .AddValidationStep<RequestValidator>()
                .AddValidationStep<ResponseValidator>()
                .AddValidationStep<SchemaValidator>()
                .Build();

        public IReadOnlyList<ValidationError> Errors => _context.Errors;

        public bool Validate()
        {
            _validator.Validate(_context);
            return !_context.Errors.Any();
        }
    }
}