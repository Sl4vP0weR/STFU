namespace STFU.Filters;

/// <summary>
/// Filter that handles <see cref="ActionException"/>s.
/// </summary>
public class ExceptionFilter : IActionFilter, IOrderedFilter
{
    public const int ExecutionOrder = int.MaxValue - 10;
    public int Order => ExecutionOrder;

    public const int DefaultExceptionStatusCode = StatusCodes.Status500InternalServerError;

    private static readonly IActionResult DefaultResult = 
        new StatusCodeResult(DefaultExceptionStatusCode);
    
    public void OnActionExecuting(ActionExecutingContext context) { }
    
    public void OnActionExecuted(ActionExecutedContext context)
    {
        var http = context.HttpContext;
        
        var exception = context.Exception;
        
        if(exception is null) return;

        IActionResult? result;

        if (exception is ActionException actionException)
            result = actionException.ToResult(context);
        else
        {
            if (InDevelopment) return;

            SentrySdk.CaptureException(exception);
            Log.Error(exception, "Unhandled exception");
            
            result = DefaultResult;
        }
        
        context.Result = result;

        context.ExceptionHandled = true;
    }
}