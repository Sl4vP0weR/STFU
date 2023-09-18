namespace STFU;

partial class StartupExtensions
{
    public static void ConfigureControllers(MvcOptions options)
    {
        AddFilters(options);
    }
}