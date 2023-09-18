namespace STFU;

partial class StartupExtensions
{
    public const string SentryDsnSection = "SentryAPI:Dsn";
    
    public static WebApplicationBuilder AddSerilog(WebApplicationBuilder builder)
    {
        // we want to use logging even if application is not running yet
        Log.Logger = ConfigureSerilog(new(), builder.Configuration).CreateLogger();
        
        builder.Host.UseSerilog(ConfigureSerilog);

        return builder;
    }

    private static void ConfigureSerilog(
        HostBuilderContext context, 
        IServiceProvider services, 
        LoggerConfiguration configuration)
    {
        ConfigureSerilog(configuration.ReadFrom.Services(services), context.Configuration);
    }
    
    private static LoggerConfiguration ConfigureSerilog(LoggerConfiguration logger, IConfiguration configuration) =>
        logger.ReadFrom.Configuration(configuration);
    
    /// <exception cref="ArgumentNullException">Configuration section was empty.</exception>
    public static void AddSentry(WebApplicationBuilder builder)
    {
        if (InDevelopment) return;

        var section = SentryDsnSection;
        var dsn = builder.Configuration[section];

        if (string.IsNullOrWhiteSpace(dsn))
            throw new ArgumentNullException(
                nameof(dsn), 
                $"Dsn for Sentry API is required, configuration section '{section}' was empty."
            );
        
        builder.Services.AddSentry();
        builder.WebHost.UseSentry(options =>
        {
            options.Dsn = dsn;
            options.TracesSampleRate = 1.0;
        });
    }
}