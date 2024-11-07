namespace FluentOpenApiValidation
{
    public interface IContract
    {
        bool Validate();

        IReadOnlyList<ValidationError> Errors { get; }
    }
}