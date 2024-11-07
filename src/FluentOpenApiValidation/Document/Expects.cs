namespace FluentOpenApiValidation
{
    internal class ExpectGet : ExpectBase, IOperationBuilder
    {
        public ExpectGet(IReadOnlyCollection<Parameter>? parameters, IDocument document, Security? security) : base(document, security)
        {
            document.Parameters = parameters;
        }

        public override HttpMethod Method => HttpMethod.Get;
    }

    internal class ExpectPut : ExpectBase, IOperationBuilder
    {
        public ExpectPut(Request request, IDocument document, Security? security) : base(document, security)
        {
            document.Request = request;
        }

        public override HttpMethod Method => HttpMethod.Put;
    }

    internal class ExpectPost : ExpectBase, IOperationBuilder
    {
        public ExpectPost(Request request, IDocument document, Security? security) : base(document, security)
        {
            document.Request = request;
        }

        public override HttpMethod Method => HttpMethod.Post;
    }

    internal class ExpectPatch : ExpectBase, IOperationBuilder
    {
        public ExpectPatch(IReadOnlyCollection<Parameter>? parameters, IDocument document, Security? security) : base(document, security)
        {
        }

        public override HttpMethod Method => HttpMethod.Patch;
    }

    internal class ExpectDelete : ExpectBase, IOperationBuilder
    {
        public ExpectDelete(IReadOnlyCollection<Parameter>? parameters, IDocument document, Security? security) : base(document, security)
        {
        }

        public override HttpMethod Method => HttpMethod.Delete;
    }
}