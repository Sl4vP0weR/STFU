namespace STFU;

partial class StartupExtensions
{
    public static MvcOptions AddFilters(MvcOptions options)
    {
        var filters = options.Filters;
        
        filters.Add<ExceptionFilter>();
        
        return options;
    }
}