namespace STFU.Tests.Performance;

public static class Program
{
    public static void Main(string[] args)
    {
        NBomberRunner.RegisterScenarios(
            Scenarios.Generation.Create(),
            Scenarios.Redirection.Create()
        ).Run(args);
    }
}