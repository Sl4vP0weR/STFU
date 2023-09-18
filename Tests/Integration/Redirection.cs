namespace STFU.Tests.Integration;

[Order(2)]
public class Redirection : IntegrationTest
{
    public Task<HttpResponseMessage> Redirect(string route) =>
        DefaultClient.GetAsync(RootController.RedirectionPrefix+route).WithLogging();
    
    [Test]
    [TestCase(GenerateRuleRequest.DefaultURL)]
    public async Task RedirectSuccessful(string url)
    {
        var response = await Generation.Generate(url);
        var path = await response.Content.ReadAsStringAsync();

        response = await DefaultClient.GetAsync(path).WithLogging();
        
        response.AssertStatusCode(Is.AnyOf(
            HttpStatusCode.Redirect, 
            HttpStatusCode.TemporaryRedirect, 
            HttpStatusCode.PermanentRedirect)
        );
    }
    
    [Test]
    [TestCase("invalid route")]
    public async Task RedirectInvalidRoute(string route)
    {
        var response = await Redirect(route);
        
        response.AssertStatusCode(Is.EqualTo(HttpStatusCode.NotFound));
    }
}