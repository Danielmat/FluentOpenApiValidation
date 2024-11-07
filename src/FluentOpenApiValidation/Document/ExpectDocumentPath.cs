namespace FluentOpenApiValidation
{
    internal class ExpectDocumentPath : IDocumentPathBuilder
    {
        public ExpectDocumentPath(string path, IDocument document)
        {
            _document = document;

            document.Path = path;
        }

        private readonly IDocument _document;

        public IOperationBuilder WithGet(OperationOptions options = null) => new ExpectGet(options.Parameters, _document, options.Security);

        public IOperationBuilder WithPatch(OperationOptions options = null)
        => new ExpectPatch(options.Parameters, _document, options.Security);

        public IOperationBuilder WithPost(PostOperationOptions options)
       => new ExpectPost(options.Request, _document, options.Security);

        public IOperationBuilder WithPut(PostOperationOptions options)
        => new ExpectPut(options.Request, _document, options.Security);

        public IOperationBuilder WithDelete(OperationOptions options = null)
        => new ExpectDelete(options.Parameters, _document, options.Security);
    }
}