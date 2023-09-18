namespace STFU;

public static partial class Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void TryThrow(this Exception? exception) => exception?.Throw();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Throw(this Exception exception) => throw exception;
}