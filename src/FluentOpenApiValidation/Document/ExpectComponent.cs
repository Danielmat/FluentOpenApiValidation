namespace FluentOpenApiValidation
{
    internal class ExpectComponent : IComponentBuilder
    {
        protected readonly IDocument _document;

        public ExpectComponent(IDocument document)
        {
            _document = document;
        }

        public IContract Build() => new FinalContractBuilder(_document)
                                       .Build();

        public IComponentBuilder WillSchema(Schema? schema = null)
        {
            _document.Schema = schema;

            return this;
        }

        public IComponentBuilder WillSecuritySchema(SecuritySchema? securitySchema = null)
        {
            _document.SecuritySchema = securitySchema;

            return this;
        }
    }

    internal class FinalContractBuilder(IDocument document)
    {
        private readonly IDocument _document = document;

        public IContract Build() => new Contract(_document);
    }
}