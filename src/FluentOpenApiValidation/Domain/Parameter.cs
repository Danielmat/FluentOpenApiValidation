namespace FluentOpenApiValidation
{
    public struct Parameter(string name, PropertyType propertyType, In inParam, bool required = false)
    {
        public string Name { get; init; } = name;
        public bool Required { get; private set; } = required;
        public PropertyType Type { get; private set; } = propertyType;
        public In InParameter { get; private set; } = inParam;
    }
}