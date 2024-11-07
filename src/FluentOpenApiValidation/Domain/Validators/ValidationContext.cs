namespace FluentOpenApiValidation
{
    internal sealed record ValidationContext(IDocument Document)
    {
        private readonly List<ValidationError> _errors = [];
        public IReadOnlyList<ValidationError> Errors => _errors;

        public void AddError(string code, string message = "") => _errors.Add(new(code, message));
    }
}