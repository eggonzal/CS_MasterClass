using StarWarsPlanetsStats.App;
using System.Reflection;

namespace StarWarsPlanetsStats;

public class UniversalTableConsolePrinter : IUniversalTablePrinter
{
    private static readonly string ColSeparator = "|";
    public void PrintTable<T>(IEnumerable<T> items, int colSize)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        PrintHeader(properties, colSize);
        foreach (var item in items)
            PrintRow(properties, item, colSize);
        
    }
    private static void PrintHeader(PropertyInfo[] properties, int colSize)
    {
        foreach (var prop in properties)
        {
            Console.Write($"{FormatFixed(prop.Name, colSize)}{ColSeparator}");
        }
        Console.WriteLine();
        Console.WriteLine(new string('-', (colSize+1) * properties.Length));
    }
    private static void PrintRow<T>(PropertyInfo[] properties, T item, int colSize)
    {
        foreach (var prop in properties)
        {
            var value = prop.GetValue(item)?.ToString() ?? string.Empty;
            Console.Write($"{FormatFixed(value, colSize)}{ColSeparator}");
        }
        Console.WriteLine();
    }
    private static string FormatFixed(string text, int colSize)
    {
        return text.Length > colSize ? string.Concat(text.AsSpan(0, colSize - 3), "...") : text.PadRight(colSize);
    }
}