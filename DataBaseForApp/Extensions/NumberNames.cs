namespace DataBase.Extensions;

public static class NumberNames
{
    public static string Name(this int number) => number switch
    {
        0 => "ноль",
        1 => "один",
        2 => "два",
        3 => "три",
        4 => "четыре",
        5 => "пять",
        6 => "шесть",
        7 => "семь",
        8 => "восемь",
        9 => "девять",
        10 => "десять",
        _ => "много",
    };
    
    public static IEnumerable<int> GetEnumerable(this int number)
    {
        for (var i = 1;  i <= number; i++) yield return i;
    } 
}