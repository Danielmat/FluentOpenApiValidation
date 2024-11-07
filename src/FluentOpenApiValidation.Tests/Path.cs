namespace FluentOpenApiValidationTest
{
    internal class FilePath
    {
        public const string PATH = @".\openapi.json";

        public static string LoadFile()
        {
            return System.IO.File.ReadAllText(PATH);
        }
    }
}