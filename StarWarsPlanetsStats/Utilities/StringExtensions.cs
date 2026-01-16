namespace StarWarsPlanetsStats.Utilities;

public static class StringExtensions
{
    public static int? ToIntOrNull(this string? value)
    {
        return int.TryParse(value, out int intVal) ? intVal : null;
    }
    public static long? ToLongOrNull(this string? value)
    {
        return long.TryParse(value, out long longVal) ? longVal : null;
    }
}