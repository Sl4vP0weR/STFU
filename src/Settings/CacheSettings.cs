namespace STFU.Settings;

public class CacheSettings
{
    public const int 
        DefaultDuration = 60;
    public static readonly TimeSpan 
        DefaultDurationSpan = TimeSpan.FromSeconds(DefaultDuration);
}