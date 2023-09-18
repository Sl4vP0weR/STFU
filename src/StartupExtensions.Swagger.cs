using Swashbuckle.AspNetCore.SwaggerGen;

namespace STFU;

partial class StartupExtensions
{
    public static IServiceCollection AddSwagger(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(ConfigureSwagger);

        return services;
    }

    public static void ConfigureSwagger(SwaggerGenOptions options)
    {
        options.EnableAnnotations();
    }
}