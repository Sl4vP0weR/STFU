using static STFU.DataBase.DataBaseContext;

namespace STFU;

partial class StartupExtensions
{
    /// <exception cref="ArgumentNullException">Configuration section was empty.</exception>
    public static IServiceCollection AddDataBase(IServiceCollection services, IConfiguration configuration)
    {
        var section = ConnectionStringSection;
        var connectionString = configuration[section];
        
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentNullException(nameof(connectionString), 
                $"Connection string for database required, configuration section '{section}' was empty.");
        
        services.AddDbContext<DataBaseContext>(ctx =>
        {
            ctx.UseNpgsql(connectionString);
        });

        return services;
    }
}