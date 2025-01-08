using System.Reflection;
using System.Text.Json;
using System.Xml;

namespace MSCaddie.Shared.Extensions;

public static class StringExtensions
{
    public static string RemoveCrLf(this string input)
    {
        return input.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("  ", " ");
    }

    public static string TrimSize(this string input, int size)
    {
        if (size < 0 || string.IsNullOrEmpty(input)) return input;

        return input[..Math.Min(input.Length, size)];
    }

    public static string RemoveWhitespace(this string input)
    {
        return new string(input.ToCharArray()
            .Where(c => !Char.IsWhiteSpace(c))
            .ToArray());
    }

    public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => "",
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };

    public static void WriteCSV<T>(this IEnumerable<T> items, string path)
    {
        Type itemType = typeof(T);
        var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .OrderBy(p => p.Name);

        using var writer = new StreamWriter(path);
        writer.WriteLine(string.Join(";", props.Select(p => p.Name)));

        foreach (var item in items)
        {
            writer.WriteLine(string.Join(";", props.Select(p => p.GetValue(item, null))));
        }
    }

    public static T GetEnum<T>(this string value) where T : struct
    {
        T x;
        if (Enum.TryParse<T>(value, true, out x))
            return x;
        return default(T);
    }
}

