using static STFU.DependencyInjection;

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
        
        builder.AddSerilog();
        
        builder.AddSentry();

        services.AddSwagger();

        services.AddSettings(configuration);
        
        services.AddOutputCache(ConfigureOutputCache);
        
        services.AddMemoryCache();

        services.AddResponseCompression();
        
        services.AddDataBase(configuration);

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