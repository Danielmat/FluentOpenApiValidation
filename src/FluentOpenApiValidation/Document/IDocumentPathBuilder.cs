namespace FluentOpenApiValidation
{
    public interface IDocumentPathBuilder
    {
        IOperationBuilder WithGet(OperationOptions options = default);

        IOperationBuilder WithPatch(OperationOptions options = default);

        IOperationBuilder WithPost(PostOperationOptions options);

        IOperationBuilder WithPut(PostOperationOptions options);

        IOperationBuilder WithDelete(OperationOptions options = default);
    }
}