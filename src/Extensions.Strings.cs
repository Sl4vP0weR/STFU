namespace STFU;

public static partial class Extensions
{
    /// <summary>
    /// Ensures input string is not null.
    /// </summary>
    public static string EnsureSafe(this string? str) => str ?? "";
    
    public static string TrimStart(this string str, string trimString)
    {
        var result = new StringBuilder(str);
        
        while ((str = result.ToString()).StartsWith(trimString))
            result.Remove(0, trimString.Length);

        return str;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrWhiteSpace(this string? text) => string.IsNullOrWhiteSpace(text);

    public static string ToBase64String(this Guid id)
    {
        var bytes = id.ToByteArray();
        var text = Convert.ToBase64String(bytes);

        return text;
    }

    [GeneratedRegex(@"([^\da-zA-Z])+")]
    public static partial Regex NotAlphanumericRegex();
    
    /// <summary>
    /// Removes all not alphanumeric characters from <paramref name="text"/>.
    /// </summary>
    /// <param name="text"></param>
    public static string ToAlphanumeric(this string text) =>
        NotAlphanumericRegex().Replace(text, "");

    /// <summary>
    /// Converts GUID <paramref name="id"/> to Base64 string with only alphanumeric characters.
    /// </summary>
    /// <param name="id"></param>
    public static string ToAlphanumericString(this Guid id) =>
        id.ToBase64String().ToAlphanumeric();
    
    public static string RandomAlphanumericString(int length)
    {
        StringBuilder builder = new(length);
        
        while (builder.Length < length)
        {
            var randomAlphanumeric = Guid.NewGuid().ToAlphanumericString();
            builder.Append(randomAlphanumeric);
        }

        var result = builder.ToString();
        
        return result[..length];
    }

    public static string? CapitalizeFirstLetter(this string? text)
    {
        if (string.IsNullOrWhiteSpace(text)) return text;

        StringBuilder result = new(text);

        var length = text!.Length;
        for (var i = 0; i < length; i++)
        {
            var character = text[i];

            if (!char.IsLetter(character))
                continue;

            result[i] = char.ToUpper(character);
            break;
        }

        return result.ToString();
    }
}