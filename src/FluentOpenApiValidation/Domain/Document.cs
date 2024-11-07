using System.Text.Json;

namespace FluentOpenApiValidation
{
    internal class Document : IDocument
    {
        public Schema Schema { get; set; }
        public IReadOnlyCollection<Response> Responses { get; set; }
        public HttpMethod Method { get; set; }
        public JsonElement Element { get; set; }
        public string Path { get; set; }
        public IReadOnlyCollection<Parameter> Parameters { get; set; }
        public Request Request { get; set; }
        public Security? Security { get; set; }
        public SecuritySchema? SecuritySchema { get; set; }
    }
}