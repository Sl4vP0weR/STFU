namespace STFU;

partial class StartupExtensions
{
    public static IServiceCollection AddSettings(IServiceCollection services, IConfiguration configuration)
    {
         void Configure<T>(string? sectionName = null)
            where T : class
        {
            var section = configuration.GetSection(sectionName ??= typeof(T).Name);
            services.Configure<T>(section);
        }
        
        Configure<CacheSettings>();
        Configure<RedirectionSettings>();
        Configure<LoggerConfiguration>(nameof(Serilog));

        return services;
    }
}