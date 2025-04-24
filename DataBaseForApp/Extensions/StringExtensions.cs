namespace DataBase.Extensions;

public static class StringExtensions
{
    public static string Repeating(this string str, int count)
    {
        var result = "";
        for (var i = 0; i < count; i++)
        {
            result += str;
        }
        return result;
    }
}