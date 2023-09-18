namespace STFU.Controllers;

[ApiController]
[Route("/")]
public class RootController(
        DataBaseContext DataBase,
        IOptions<RedirectionSettings> RedirectionSettings)
    : ControllerBase
{
    public const string RedirectionPrefix = "r/";
    
    [SwaggerOperation("Redirect", "Redirects to the URL bound to the provided redirection rule route.")]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status302Found)]
    [HttpGet($"{RedirectionPrefix}{{{nameof(route)}}}")]
    [OutputCache(PolicyName = CachePolicies.Always.Name)]
    public async Task Get([FromRoute] string route)
    {
        var rule = await DataBase.FindRule(route);
        var url = rule?.URL;

        if (url.IsNullOrWhiteSpace())
            Response.StatusCode = StatusCodes.Status404NotFound;
        else Response.Redirect(url!);
    }

    [SwaggerOperation("Generate", "Generates redirection rule for future use.")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [HttpGet(nameof(Generate))]
    [OutputCache(PolicyName = CachePolicies.Always.Name)]
    public async Task<string> Generate([FromQuery] GenerateRuleRequest request)
    {
        request.Validate(out var url).TryThrow();

        ValidateHost(url).TryThrow();

        var rule = await DataBase.GetOrAddRule(url);

        return GetAbsoluteRedirectionURL(rule);
    }

    private ActionException? ValidateHost(Uri url)
    {
        var host = url.Host;

        var settings = RedirectionSettings.Value;

        var notSupported = ActionException.ArgumentNotSupported(host);

        if (!settings.IsHostAllowed(Request, host))
            return notSupported;

        return null;
    }

    private string GetAbsoluteRedirectionURL(RedirectionRule rule) =>
        GetAbsoluteRedirectionURL(Request, rule);
    private static string GetAbsoluteRedirectionURL(HttpRequest request, RedirectionRule rule) =>
        $"{request.Scheme}://{request.Host}/{RedirectionPrefix}{rule.Route}";
}