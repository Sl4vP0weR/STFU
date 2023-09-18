namespace STFU.Tests.Integration;

[TestFixture]
public abstract class IntegrationTest
{
    internal static readonly WebApplicationFactory<STFU.Startup> ApplicationFactory = new();
    
    [OneTimeSetUp]
    public static void SetUp()
    {
        DefaultClient = CreateClient();
    }

    [OneTimeTearDown]
    public static void TearDown()
    {
        DefaultClient?.Dispose();
    }
    
    public static HttpClient DefaultClient { get; protected set; }
    
    public static readonly WebApplicationFactoryClientOptions DefaultClientOptions = new()
    {
        AllowAutoRedirect = false,
    };
    
    public static HttpClient CreateClient(WebApplicationFactoryClientOptions? options = null) => 
        ApplicationFactory.CreateClient(options ?? DefaultClientOptions);
}