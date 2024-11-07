namespace FluentOpenApiValidation
{
    public interface IOperationBuilder
    {
        IResponseBuilder WillRespond(IReadOnlyCollection<Response> responses);
    }
}