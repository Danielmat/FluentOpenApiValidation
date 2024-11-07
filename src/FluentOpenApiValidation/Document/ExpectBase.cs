namespace FluentOpenApiValidation
{
    internal abstract class ExpectBase
    {
        private readonly IDocument _document;

        protected ExpectBase(IDocument document, Security? security = null)
        {
            _document = document;

            _document.Method = Method;

            _document.Security = security;
        }

        public IResponseBuilder WillRespond(IReadOnlyCollection<Response> responses)
        {
            return new ExpectResponse(responses, _document);
        }

        public abstract HttpMethod Method { get; }
    }
}