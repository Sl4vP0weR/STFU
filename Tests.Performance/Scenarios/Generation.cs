namespace STFU.Tests.Performance.Scenarios;

public class Generation : IScenarioDescriptor
{
    public static string Name => nameof(Generation);
    
    public static ScenarioProps Create() =>
        Scenario
            .Create(Name, Execute)
            .WithoutWarmUp()
            .WithLoadSimulations(Simulation.KeepConstant(copies: 10, TimeSpan.FromSeconds(30)));

    public static readonly AlphanumericValueGenerator ValueGenerator = new() { Length = 8 };

    public static async Task<IResponse> Execute(IScenarioContext context)
    {
        using var httpClient = CreateClient();
        var url = "https://example.com/" + ValueGenerator.Next();
        var request = Http.CreateRequest(HttpMethods.Get, $"Generate?URL={url}");

        var response = await Http.Send(httpClient, request);

        return response;
    }

    public static NodeStats Run() => NBomberRunner.RegisterScenarios(Create()).WithReportFileName(Name).Run();
}