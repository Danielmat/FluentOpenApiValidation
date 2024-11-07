using System.Text.Json;

namespace FluentOpenApiValidation
{
    internal class ExpectFileDocument(IDocument document) : ILoadDocumentBuilder
    {
        private readonly IDocument _document = document;

        public ExpectFileDocument LoadFromSource(string path)
        {
            _document.Element = JsonSerializer.Deserialize<JsonElement>(File.ReadAllText(path));

            return this;
        }

        public ExpectFileDocument LoadFile(string file)
        {
            _document.Element = JsonSerializer.Deserialize<JsonElement>(file);

            return this;
        }

        public IDocumentPathBuilder WithPath(string path)
       => string.IsNullOrEmpty(path) ? throw new ArgumentException(null, nameof(path))
            : new ExpectDocumentPath(path, _document);
    }
}