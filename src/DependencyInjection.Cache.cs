namespace STFU;

public static partial class DependencyInjection
{
    public static void ConfigureOutputCache(OutputCacheOptions options)
    {
        options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(CacheSettings.DefaultDuration);
        
        CachePolicies.Always.Add(options);
    }
}