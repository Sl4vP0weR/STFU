namespace STFU.DataBase;

public class DataBaseContext : DbContext
{
    public const string ConnectionStringSection = "PostgreSQL:ConnectionString";

    public DataBaseContext() { }

    public DataBaseContext(DbContextOptions options) : base(options) { }

    public DbSet<RedirectionRule> Rules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder model)
    {
        base.OnModelCreating(model);

        ConfigureEntities(model);
    }

    public static void ConfigureEntities(ModelBuilder modelBuilder)
    {
        ConfigureEntity(modelBuilder.Entity<RedirectionRule>());
    }

    private static void ConfigureEntity(EntityTypeBuilder<RedirectionRule> entity)
    {
        entity.HasKey(x => x.Route);

        entity.Property(x => x.Route)
            .IsRequired()
            .HasMaxLength(100)
            .HasValueGenerator((property, entityType) => routeValueGenerator);

        entity.Property(x => x.URL)
            .IsRequired()
            .HasMaxLength(2048);

        entity.HasIndex(x => x.Route).IsUnique();
        entity.HasIndex(x => x.URL).IsUnique();
    }

    private static readonly Func<DataBaseContext, string?, Task<RedirectionRule?>> 
        findRuleByURL = EF.CompileAsyncQuery((DataBaseContext db, string? url) =>
            db.Rules.AsNoTracking().FirstOrDefault(x => x.URL == url));

    public Task<RedirectionRule?> FindRule(Uri url) => findRuleByURL(this, url.ToString());

    public Task<RedirectionRule?> FindRule(string? route) => Rules.FindAsync(route).AsTask();

    private readonly static AlphanumericValueGenerator routeValueGenerator = new()
    {
        Length = 8
    };
    
    public async Task<RedirectionRule> AddRule(Uri url, string? route = null)
    {
        var rule = new RedirectionRule(url, route);

        await Rules.AddAsync(rule);
        await SaveChangesAsync();

        return rule;
    }

    public async Task<RedirectionRule> GetOrAddRule(Uri url, string? route = null) =>
        await FindRule(url) ?? await AddRule(url, route);
}