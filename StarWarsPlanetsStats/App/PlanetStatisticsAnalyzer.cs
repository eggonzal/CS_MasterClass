using StarWarsPlanetsStats.Model;
using StarWarsPlanetsStats.UserInteraction;

namespace StarWarsPlanetsStats.App;

public class PlanetStatisticsAnalyzer(IPlanetsStatsUserInteractor planetsStatsUserInteractor) : IPlanetStatisticsAnalyzer
{
    private readonly IPlanetsStatsUserInteractor _planetsStatsUserInteractor = planetsStatsUserInteractor;

    public void Analyze(IEnumerable<Planet> planets)
    {
        var propertyNamesToSelectorsMapping = new Dictionary<string, Func<Planet, long?>>()
    {
        { "diameter", p => p.Diameter },
        { "surface water", p => p.SurfaceWater },
        { "population", p => p.Population }
    };
        _planetsStatsUserInteractor.ShowMessage(Environment.NewLine);
        string? userChoice = _planetsStatsUserInteractor.ChooseStatisticsToBeShown(propertyNamesToSelectorsMapping.Keys);
        while (string.IsNullOrEmpty(userChoice) || !propertyNamesToSelectorsMapping.ContainsKey(userChoice))
        {
            _planetsStatsUserInteractor.ShowMessage("Invalid choice. Please enter one of the following properties: " + string.Join(", ", propertyNamesToSelectorsMapping.Keys));
            userChoice = _planetsStatsUserInteractor.ChooseStatisticsToBeShown(propertyNamesToSelectorsMapping.Keys);
        }
        ShowStatistics(planets, userChoice, propertyNamesToSelectorsMapping[userChoice]);
    }
    private void ShowStatistics(IEnumerable<Planet> planets, string propertyName, Func<Planet, long?> propertySelector)
    {
        var maxPlanetByProperty = planets.MaxBy(propertySelector);
        _planetsStatsUserInteractor.ShowMessage($"Max {propertyName} is {propertySelector(maxPlanetByProperty)} (planet: {maxPlanetByProperty.Name})");
        var minPlanetByProperty = planets.MinBy(propertySelector);
        _planetsStatsUserInteractor.ShowMessage($"Min {propertyName} is {propertySelector(minPlanetByProperty)} (planet: {minPlanetByProperty.Name})");
    }
}