namespace STFU.Services;

public class RuleRepository(DataBaseContext db) : IRuleRepository
{
    private static readonly Func<DataBaseContext, string?, Task<RedirectionRule?>> 
        findRuleByURL = EF.CompileAsyncQuery((DataBaseContext db, string? url) =>
            db.Rules.AsNoTracking().FirstOrDefault(x => x.URL == url));

    public Task<RedirectionRule?> Find(Uri url) => findRuleByURL(db, url.ToString());

    public Task<RedirectionRule?> Find(string? route) => db.Rules.FindAsync(route).AsTask();
    
    public async Task<RedirectionRule> Add(RedirectionRule rule)
    {
        await db.Rules.AddAsync(rule);
        await db.SaveChangesAsync();

        return rule;
    }

    public async Task<RedirectionRule> Ensure(Uri url, string? route = null) =>
        await Find(url) ?? await Find(route) ?? await Add(new(url, route));
}