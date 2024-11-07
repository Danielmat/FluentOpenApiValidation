namespace FluentOpenApiValidation
{
    public struct Request
    {
        public readonly MediaTypes MediaTypes;

        public Request(MediaTypes mediaTypes)
        {
            ArgumentNullException.ThrowIfNull(mediaTypes);

            MediaTypes = mediaTypes;
        }
    }
}