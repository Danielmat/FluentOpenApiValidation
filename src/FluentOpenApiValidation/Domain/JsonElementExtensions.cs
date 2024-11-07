using System.Text.Json;

internal static class JsonElementExtensions
{
    public static bool TryGetPath(this JsonElement element, ReadOnlySpan<char> path, out JsonElement result)
    {
        result = element;
        var remaining = path;

        while (remaining.Length > 0)
        {
            var separatorIndex = remaining.IndexOf('.');
            var segment = separatorIndex == -1 ? remaining : remaining[..separatorIndex];

            if (!result.TryGetProperty(segment, out result))
                return false;

            if (separatorIndex == -1)
                break;

            remaining = remaining[(separatorIndex + 1)..];
        }

        return true;
    }
}