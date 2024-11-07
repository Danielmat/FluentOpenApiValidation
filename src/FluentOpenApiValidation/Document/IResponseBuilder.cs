namespace FluentOpenApiValidation
{
    public interface IResponseBuilder : IComponent
    { }

    public interface IComponent
    {
        IComponentBuilder WillSchema(Schema schema = null);

        IComponentBuilder WillSecuritySchema(SecuritySchema? securitySchema = default);
    }
}