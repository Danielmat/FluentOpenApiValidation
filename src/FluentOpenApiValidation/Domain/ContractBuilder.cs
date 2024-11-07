namespace FluentOpenApiValidation
{
    public class ContractBuilder
    {
        public static ILoadDocumentBuilder WithFileSource(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException(null, nameof(filePath));

            return new ExpectFileDocument(new Document()).LoadFromSource(filePath);
        }

        public static ILoadDocumentBuilder WithFile(Func<string> jsonFile)
        {
            return new ExpectFileDocument(new Document()).LoadFile(jsonFile());
        }
    }
}