namespace STFU.Settings;

public class RedirectionSettings
{
    public bool AllowSameHost { get; set; }
    public string[] DisallowedHosts { get; set; } = Array.Empty<string>();
    
    public bool IsHostAllowed(HttpRequest request, string host)
    {
        if(!AllowSameHost)
            if (host.Equals(request.Host.Host, StringComparison.OrdinalIgnoreCase))
                return false;

        return IsHostAllowed(host);
    }

    public bool IsHostAllowed(string host) =>
        !DisallowedHosts.Any(x => x.Equals(host, StringComparison.OrdinalIgnoreCase));
}