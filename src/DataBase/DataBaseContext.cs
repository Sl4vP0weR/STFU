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
            .HasValueGenerator((property, entityType) => RouteValueGenerator);

        entity.Property(x => x.URL)
            .IsRequired()
            .HasMaxLength(2048);

        entity.HasIndex(x => x.Route).IsUnique();
        entity.HasIndex(x => x.URL).IsUnique();
    }
    
    public readonly static AlphanumericValueGenerator RouteValueGenerator = new()
    {
        Length = 8
    };
}