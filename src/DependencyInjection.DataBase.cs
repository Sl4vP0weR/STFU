namespace STFU;

public static partial class DependencyInjection
{
    public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataBaseContext>(ctx =>
        {
            ctx.UseNpgsql(configuration[DataBaseContext.ConnectionStringSection]);
        });

        return services;
    }
}