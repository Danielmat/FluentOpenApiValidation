namespace FluentOpenApiValidation
{
    public struct Property(string name, PropertyType? type = null, PropertyFormat? format = null)
    {
        public PropertyType? Type { get; private set; } = type;
        public string Name { get; private set; } = name;
        public PropertyFormat? Format { get; private set; } = format;
    }
}