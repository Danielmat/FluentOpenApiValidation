namespace FluentOpenApiValidation
{
    public interface IComponentBuilder : IComponent
    {
        IContract Build();
    }
}