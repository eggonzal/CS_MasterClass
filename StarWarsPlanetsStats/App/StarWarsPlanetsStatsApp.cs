using StarWarsPlanetsStats.DataAccess;
using StarWarsPlanetsStats.Model;

namespace StarWarsPlanetsStats.App;

public class StarWarsPlanetsStatsApp(IPlanetsReader planetsReader, IUniversalTablePrinter universalTableConsolePrinter, IPlanetStatisticsAnalyzer planetStatisticsAnalyzer)
{
    private readonly IUniversalTablePrinter _universalTableConsolePrinter = universalTableConsolePrinter;
    private readonly IPlanetsReader _planetsReader = planetsReader;
    private readonly IPlanetStatisticsAnalyzer _planetStatisticsAnalyzer = planetStatisticsAnalyzer;

    public async Task Run()
    {
        IEnumerable<Planet> planets = await _planetsReader.Read();
        _universalTableConsolePrinter.PrintTable(planets!, 20);
        _planetStatisticsAnalyzer.Analyze(planets);
    }
}