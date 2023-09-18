namespace STFU;

public static partial class Extensions
{
    /// <summary>
    /// Null reference safe <see cref="ICollection.Count"/> property accessor.
    /// </summary>
    public static int GetCount(this ICollection? collection) => collection?.Count ?? 0;
}