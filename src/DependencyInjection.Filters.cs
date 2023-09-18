namespace STFU;

public static partial class DependencyInjection
{
    public static MvcOptions AddFilters(this MvcOptions options)
    {
        var filters = options.Filters;
        
        filters.Add<ExceptionFilter>();
        
        return options;
    }
}