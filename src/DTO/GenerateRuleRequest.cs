namespace STFU.DTO;

public class GenerateRuleRequest
{
    public const string
        DefaultURL = "https://example.com/";
    
    public const int 
        URL_MaxLength = 100;
    
    public static readonly List<string> SupportedSchemes = new()
    {
        "http", 
        "https"
    };
    
    [DefaultValue(DefaultURL)]
    public string? URL { get; set; }

    public ActionException? Validate(out Uri url)
    {
        url = null!;

        if (URL.IsNullOrWhiteSpace())
            return ActionException.ArgumentNull(URL);

        if (URL.Length > URL_MaxLength)
            return ActionException.ArgumentLength(URL, ..URL_MaxLength);

        if (!Uri.TryCreate(URL, UriKind.Absolute, out url!))
            return ActionException.ArgumentWrongFormatWithCorrection(URL, "{Scheme}://{Domain}/{Route}");

        var scheme = url.Scheme;
        if (!SupportedSchemes.Contains(scheme))
            return ActionException.ArgumentNotSupported(scheme);

        return null;
    }
}