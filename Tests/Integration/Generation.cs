namespace STFU.Tests.Integration;

[Order(1)]
public class Generation : IntegrationTest
{
    public static Task<HttpResponseMessage> Generate(string url) =>
        DefaultClient.GetAsync($"Generate?URL={url}").WithLogging();
    
    [Test]
    [TestCase(GenerateRuleRequest.DefaultURL)]
    public static async Task GenerateSuccessful(string url)
    {
        var response = await Generation.Generate(url);
        
        response.AssertStatusCode(Is.EqualTo(HttpStatusCode.OK));

        var uri = await response.Content.AsUriAsync();
        Assert.NotNull(uri, "Invalid content format. Should be an absolute URI.");
    }

    [Test]
    [TestCase("invalid://url/")]
    [TestCase("invalid url")]
    public static async Task GenerateInvalid(string url)
    {
        var response = await Generation.Generate(url);
        
        response.AssertStatusCode(Is.EqualTo(HttpStatusCode.BadRequest));
    }
}