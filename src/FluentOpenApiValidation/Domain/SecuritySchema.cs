namespace FluentOpenApiValidation
{
    public struct SecuritySchema
    {
        public readonly string Name;

        public SecuritySchema(string name)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

            Name = name;
        }
    }
}