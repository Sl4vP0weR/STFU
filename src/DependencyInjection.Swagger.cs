using Swashbuckle.AspNetCore.SwaggerGen;

namespace STFU;

public static partial class DependencyInjection
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
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