namespace STFU.Services;

public interface IRuleRepository
{
    Task<RedirectionRule?> Find(Uri url);
    Task<RedirectionRule?> Find(string? route);
    Task<RedirectionRule> Add(RedirectionRule rule);
    Task<RedirectionRule> Ensure(Uri url, string? route = null);
}