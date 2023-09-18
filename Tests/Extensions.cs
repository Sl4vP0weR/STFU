namespace STFU.Tests;

public static class Extensions
{
    public static HttpResponseMessage AssertStatusCode(this HttpResponseMessage response, IResolveConstraint constraint)
    {
        Assert.That(
        response.StatusCode, 
        constraint, 
        "Invalid status code.");

        return response;
    }

    public static async Task<Uri?> AsUriAsync(this HttpContent content, UriKind kind = UriKind.Absolute)
    {
        var contentText = await content.ReadAsStringAsync();
        Uri.TryCreate(contentText, kind, out var uri);
        
        return uri;
    }

    public static Task<HttpResponseMessage> WithLogging(this Task<HttpResponseMessage> task) =>
        task.ContinueWith(t => t.Result.WithLogging());
    
    public static HttpResponseMessage WithLogging(this HttpResponseMessage response)
    {
        var requestUrl = response.RequestMessage!.RequestUri!;
        Log.Information($"'{requestUrl}' - {(int)response.StatusCode} ({response.ReasonPhrase})");

        string content;
        using (StreamReader memory = new(response.Content.ReadAsStream()))
            content = memory.ReadToEnd();
        
        if(!string.IsNullOrWhiteSpace(content))
            Log.Information($"Content: '{content}'");

        return response;
    }
}