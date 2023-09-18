namespace STFU;

public static partial class DependencyInjection
{
    public const string SentryDsnSection = "SentryAPI:Dsn";
    
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog(ConfigureSerilog);

        return builder;
    }

    private static void ConfigureSerilog(
        HostBuilderContext context, 
        IServiceProvider services, 
        LoggerConfiguration configuration)
    {
        configuration
            .ReadFrom.Services(services)
            .ReadFrom.Configuration(context.Configuration);
    }
    
    public static void AddSentry(this WebApplicationBuilder builder)
    {
        if (InDevelopment) return;

        builder.Services.AddSentry();
        builder.WebHost.UseSentry(options =>
        {
            options.Dsn = builder.Configuration[SentryDsnSection];
            options.TracesSampleRate = 1.0;
        });
    }
}