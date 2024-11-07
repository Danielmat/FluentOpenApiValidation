using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

public static class ContractExtensions
{
    private static readonly ConcurrentDictionary<(Type EnumType, string EnumValue), string> _descriptionCache = new();

    public static string ToDescriptionString(this Enum value)
    {
        ArgumentNullException.ThrowIfNull(value);

        var enumType = value.GetType();
        var enumValue = value.ToString();

        return _descriptionCache.GetOrAdd(
            (enumType, enumValue),
            key => GetDescription(key.EnumType, key.EnumValue));
    }

    private static string GetDescription(Type enumType, string enumValue)
    {
        try
        {
            var field = enumType.GetField(enumValue);
            if (field is null) return enumValue;

            var attribute = field.GetCustomAttribute<DescriptionAttribute>(false);
            return attribute?.Description ?? enumValue;
        }
        catch (Exception)
        {
            return enumValue;
        }
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (T element in source)
            action(element);
    }
}