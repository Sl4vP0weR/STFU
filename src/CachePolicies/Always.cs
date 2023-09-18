namespace STFU.CachePolicies;

public class Always : Custom
{
    public Always()
    {
        base.AnonymousAccessOnly = false;
        base.CookieRequired = false;
        base.AllowedMethods = Array.Empty<HttpMethod>();
        base.AllowedStatusCodes = Array.Empty<int>();
    }
    
    public const string Name = nameof(Always);
    public static readonly Always Instance = new();

    public static void Add(OutputCacheOptions options) => 
        options.AddPolicy(Name, Instance);
}