namespace STFU.Entities;

public class AlphanumericValueGenerator : ValueGenerator<string>
{
    [Range(0, int.MaxValue)] 
    public required int Length { get; init; }
    
    public bool UpperCase { get; init; }
    public bool LowerCase { get; init; }
    
    public override string Next(EntityEntry? entry = null)
    {
        var result = RandomAlphanumericString(Length);
        
        if (UpperCase)
            return result.ToUpper();
        
        if (LowerCase)
            return result.ToLower();

        return result;
    }

    public override bool GeneratesTemporaryValues => false;
}