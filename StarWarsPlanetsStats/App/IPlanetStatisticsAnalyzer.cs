using StarWarsPlanetsStats.Model;

namespace StarWarsPlanetsStats.App;

public interface IPlanetStatisticsAnalyzer
{
    void Analyze(IEnumerable<Planet> planets);
}