namespace FluentOpenApiValidation
{
    public record Schema
    {
        string schemaCaracter = "#";
        public Schema(string name, IReadOnlyList<Property> properties)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

            Name = name;
            Properties = properties;

            Apply();
        }

        private void Apply()
        {
            Name = Name.StartsWith(schemaCaracter) ? Name.Replace(schemaCaracter, string.Empty) : Name;
        }

        public string Name { get; private set; }
        public IReadOnlyList<Property> Properties { get; private set; }
    }
}