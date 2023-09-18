namespace STFU.Tests.Performance.Scenarios;

public class Redirection : IScenarioDescriptor
{
    public static string Name => nameof(Redirection);
    
    public static ScenarioProps Create() =>
        Scenario
            .Create(Name, Execute)
            .WithoutWarmUp()
            .WithLoadSimulations(Simulation.KeepConstant(copies: 10, TimeSpan.FromSeconds(30)));

    public static readonly AlphanumericValueGenerator ValueGenerator = new() { Length = 8 };

    public static async Task<IResponse> Execute(IScenarioContext context)
    {
        using var httpClient = CreateClient();
        var route = ValueGenerator.Next();
        var request = Http.CreateRequest(HttpMethods.Get, RootController.RedirectionPrefix + route);

        var response = await Http.Send(httpClient, request);

        var payload = response.Payload.Value;
        var statusCode = payload.StatusCode;
        if(statusCode == HttpStatusCode.NotFound) 
            return Response.Ok(payload);

        return response;
    }

    public static NodeStats Run() => NBomberRunner.RegisterScenarios(Create()).WithReportFileName(Name).Run();
}