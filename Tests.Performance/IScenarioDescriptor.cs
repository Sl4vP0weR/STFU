namespace STFU.Tests.Performance;

public interface IScenarioDescriptor
{    
    static abstract string Name { get; }
    static abstract Task<IResponse> Execute(IScenarioContext context);
    static abstract ScenarioProps Create();
    static abstract NodeStats Run();
}