using System.Net;

namespace FluentOpenApiValidation
{
    public struct Response(HttpStatusCode httpStatusCode, MediaTypes? media = null, SchemaType? type = null)
    {
        public readonly HttpStatusCode HttpStatusCode = httpStatusCode;
        public readonly MediaTypes? MediaTypes = media;
        public readonly SchemaType? SchemaType = type;
    }
}