namespace STFU.Entities;

public partial class RedirectionRule : IEntity
{
    public string? Route { get; set; }
    
    public string URL { get; set; }

    private RedirectionRule() {}
    public RedirectionRule(Uri url, string? route = null) : this()
    {
        URL = url.ToString();
        Route = route;
    }
    
    public override string ToString() => Route.EnsureSafe();

    public override bool Equals(object? obj) => 
        obj is RedirectionRule other && 
        Equals(other);

    protected bool Equals(RedirectionRule other) => 
        GetHashCode() == other.GetHashCode();

    public override int GetHashCode() => Route.EnsureSafe().GetHashCode();
}