namespace FluentOpenApiValidation
{
    internal class ExpectResponse : IResponseBuilder
    {
        private readonly IDocument _document;

        public ExpectResponse(IReadOnlyCollection<Response> responses, IDocument document)
        {
            _document = document;

            _document.Responses = responses;
        }

        public IComponentBuilder WillSchema(Schema? schema) => new ExpectComponent(_document).WillSchema(schema);

        public IComponentBuilder WillSecuritySchema(SecuritySchema? securitySchema)
            => new ExpectComponent(_document).WillSecuritySchema(securitySchema);
    }
}