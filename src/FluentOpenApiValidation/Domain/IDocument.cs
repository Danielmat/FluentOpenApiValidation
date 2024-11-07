using System.Text.Json;

namespace FluentOpenApiValidation
{
    public interface IDocument
    {
        JsonElement Element { get; set; }
        string Path { get; set; }
        IReadOnlyCollection<Parameter>? Parameters { get; set; }
        Schema? Schema { get; set; }
        IReadOnlyCollection<Response> Responses { get; set; }
        HttpMethod Method { get; set; }
        Request Request { get; set; }

        SecuritySchema? SecuritySchema { get; set; }
        Security? Security { get; set; }
    }
}