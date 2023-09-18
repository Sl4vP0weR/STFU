namespace STFU.Exceptions;

public static class ActionException_Extensions
{
    public static ActionException ForAction(this Exception? exception) =>
        exception as ActionException ?? new (exception?.Message);
}