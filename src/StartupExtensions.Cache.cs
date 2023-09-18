namespace STFU;

partial class StartupExtensions
{
    public static void ConfigureOutputCache(OutputCacheOptions options)
    {
        options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(CacheSettings.DefaultDuration);
        
        CachePolicies.Always.Add(options);
    }
}