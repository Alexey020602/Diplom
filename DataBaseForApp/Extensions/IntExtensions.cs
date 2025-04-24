namespace DataBase.Extensions;

public static class IntExtensions
{
    public static int GetId(this int value, int to, int from = 1) => (value - from) % to + from;
}