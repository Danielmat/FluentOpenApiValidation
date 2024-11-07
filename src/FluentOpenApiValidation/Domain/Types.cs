using System.ComponentModel;

namespace FluentOpenApiValidation
{
    public enum SchemaType
    {
        [Description("array")]
        Array
    }

    public enum PropertyType
    {
        [Description("integer")]
        Integer,

        [Description("string")]
        String,

        [Description("boolean")]
        Boolean,

        [Description("component")]
        Component,

        [Description("array")]
        Array
    }

    public enum PropertyFormat
    {
        [Description("uuid")]
        Uuid,

        [Description("int64")]
        Int64,

        [Description("int32")]
        Int32,

        [Description("date-time")]
        DateTime,
    }

    public enum MediaTypes
    {
        [Description("application/json")]
        ApplicationJson,

        [Description("text/plain; charset=utf-8")]
        TextPlainUft8,

        [Description("application/xml")]
        ApplicationXml
    }

    public enum In
    {
        [Description("query")]
        Query,

        [Description("header")]
        Header,

        [Description("Path")]
        Path,

        [Description("cookie")]
        Cookie
    }
}