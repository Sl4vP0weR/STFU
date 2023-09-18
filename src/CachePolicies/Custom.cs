namespace STFU.CachePolicies;

/// <summary>
/// Rewritten version of default policy with configurable properties.
/// </summary>
public class Custom : IOutputCachePolicy
{   
    public string VaryByQueryKeys { get; set; } = "*";
    
    public bool AllowLocking { get; set; } = true;
    
    public bool AnonymousAccessOnly { get; set; } = true;
    
    public bool CookieRequired { get; set; } = true;
    
    public HttpMethod[] AllowedMethods { get; set; } = new [] { HttpMethod.Get, HttpMethod.Head };
    
    public int[] AllowedStatusCodes { get; set; } = new [] { StatusCodes.Status200OK };
    
    public virtual ValueTask CacheRequestAsync(OutputCacheContext context, CancellationToken cancellationToken)
    {
        var attemptOutputCaching = AttemptOutputCaching(context);
        context.EnableOutputCaching = true;
        context.AllowCacheLookup = attemptOutputCaching;
        context.AllowCacheStorage = attemptOutputCaching;
        context.AllowLocking = AllowLocking;

        // Vary by any query by default
        context.CacheVaryByRules.QueryKeys = VaryByQueryKeys;

        return ValueTask.CompletedTask;
    }

    public virtual ValueTask ServeFromCacheAsync(OutputCacheContext context, CancellationToken cancellationToken) =>
        ValueTask.CompletedTask;

    public virtual ValueTask ServeResponseAsync(OutputCacheContext context, CancellationToken cancellationToken)
    {
        var response = context.HttpContext.Response;

        // Verify existence of cookie headers
        context.AllowCacheStorage = !(InvalidateCookie(response) || 
                                      InvalidateStatusCode(response));

        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// Check if the current request fulfills the requirements to be cached
    /// </summary>
    public virtual bool AttemptOutputCaching(OutputCacheContext context)
    {
        var http = context.HttpContext;
        var request = http.Request;
        var method = Enum.Parse<HttpMethod>(request.Method, true);

        if (InvalidateHttpMethod(method))
            return false;

        if (InvalidateAuthorization(request))
            return false;

        return true;
    }

    public virtual bool InvalidateAuthorization(HttpRequest request) =>
        AnonymousAccessOnly &&
        !StringValues.IsNullOrEmpty(request.Headers.Authorization) ||
        request.HttpContext.User?.Identity?.IsAuthenticated == true;
    
    public virtual bool InvalidateCookie(HttpResponse response) => 
        CookieRequired &&
        !StringValues.IsNullOrEmpty(response.Headers.SetCookie);

    public virtual bool InvalidateStatusCode(HttpResponse response) =>
        AllowedStatusCodes.GetCount() > 0 &&
        AllowedStatusCodes.Contains(response.StatusCode);

    public virtual bool InvalidateHttpMethod(HttpMethod method) =>
        AllowedMethods.GetCount() > 0 &&
        AllowedMethods.Contains(method);
}