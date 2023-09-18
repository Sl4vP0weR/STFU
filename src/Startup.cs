using static STFU.StartupExtensions;

namespace STFU;

public class Startup
{
    public static WebApplication App { get; private set; }
    public static WebApplicationBuilder Builder { get; private set; }
    public static bool InDevelopment { get; private set; }
    
    public static void Main(string[] args)
    {
        var builder = Builder = WebApplication.CreateBuilder(args);
        InDevelopment = builder.Environment.IsDevelopment();
        
        var services = builder.Services;
        var configuration = builder.Configuration;

        configuration.AddUserSecrets<AssemblyMarker>();
        
        AddSerilog(builder);

        try
        {
            AddSentry(builder);
        }
        catch (Exception exception)
        {
            Log.Warning("Sentry API is disabled due to an exception: \n{Exception}", exception);
        }

        AddServices(services);
        
        AddSwagger(services);

        AddSettings(services, configuration);

        AddDataBase(services, configuration);
        
        services.AddOutputCache(ConfigureOutputCache);
        
        services.AddMemoryCache();

        services.AddResponseCompression();

        services.AddControllers(ConfigureControllers);

        var app = App = builder.Build();

        if (InDevelopment)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseResponseCompression();
        
        app.UseOutputCache();

        app.UseHsts();
        
        app.MapControllers();

        app.Run();
    }
}