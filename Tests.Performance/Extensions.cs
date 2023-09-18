global using static STFU.Tests.Performance.Extensions;

namespace STFU.Tests.Performance;

public static class Extensions
{
    internal static readonly WebApplicationFactory<STFU.Startup> ApplicationFactory = new();

    public static readonly WebApplicationFactoryClientOptions ClientOptions = new()
    {
    };
    
    public static HttpClient CreateClient() => ApplicationFactory.CreateClient(ClientOptions);
}