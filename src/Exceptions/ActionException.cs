namespace STFU.Exceptions;

public partial class ActionException : Exception
{
    public const string DefaultMessage = "Bad request.";
    public const HttpStatusCode DefaultStatus = HttpStatusCode.BadRequest;
    
    public HttpStatusCode StatusCode { get; set; }

    public ActionException(
        string? message = null, 
        HttpStatusCode statusCode = DefaultStatus) :
        base(FormatMessage(message ?? DefaultMessage))
    {
        StatusCode = statusCode;
    }

    private static string FormatMessage(string message) =>
        message.Trim().CapitalizeFirstLetter();
    
    public override string ToString() => Message;

    public virtual IActionResult ToResult(ActionExecutedContext context) => ToResult();
    
    public virtual IActionResult ToResult() => 
        new ObjectResult(ToString()) { StatusCode = (int)StatusCode };

    public static ActionException ArgumentNull(
        object? argument, 
        [CallerArgumentExpression(nameof(argument))] string argumentExpression = null!) =>
        new ArgumentNullException(argumentExpression).ForAction();
    
    public static ActionException ArgumentWrongFormat(
        object? argument,
        [CallerArgumentExpression(nameof(argument))] string argumentExpression = null!) =>
        new(WrongFormat(FormatArgument(argument, argumentExpression)));
    
    public static ActionException ArgumentWrongFormatWithCorrection(
        object? argument, 
        string correctFormat,
        [CallerArgumentExpression(nameof(argument))] string argumentExpression = null!) =>
        new($"{WrongFormat(FormatArgument(argument, argumentExpression))}, correct format is {correctFormat}");
    
    private static string WrongFormat(string argument) =>
        $"Value of {argument} has wrong format";
    
    public static ActionException NotSupported(string message) =>
        new($"{message} is not supported");
    
    public static ActionException ArgumentNotSupported(
        object? argument, 
        [CallerArgumentExpression(nameof(argument))] string argumentExpression = null!) =>
        NotSupported($"Value of {FormatArgument(argument, argumentExpression)}");
    
    public static ActionException ArgumentLength(
        string? argument,
        Range range,
        [CallerArgumentExpression(nameof(argument))] string argumentExpression = null!) =>
        new($"Value of {FormatArgument(argument, argumentExpression)} should be in range [{range}]");
    
    public static string FormatArgument(
        object? argument,
        [CallerArgumentExpression(nameof(argument))] string? argumentExpression = null!) =>
        $"{argumentExpression} = \"{argument}\"";
}