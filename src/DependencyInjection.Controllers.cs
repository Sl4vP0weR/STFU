namespace STFU;

public static partial class DependencyInjection
{
    public static void ConfigureControllers(MvcOptions options)
    {
        options.AddFilters();
    }
}