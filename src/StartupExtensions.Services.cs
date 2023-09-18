namespace STFU;

partial class StartupExtensions
{
    public static IServiceCollection AddServices(IServiceCollection services)
    {
        services.AddScoped<IRuleRepository, RuleRepository>();
        
        return services;
    }
}