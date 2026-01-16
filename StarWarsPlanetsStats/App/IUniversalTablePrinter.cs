namespace StarWarsPlanetsStats.App;

public interface IUniversalTablePrinter
{
    void PrintTable<T>(IEnumerable<T> objs, int colSize);
}