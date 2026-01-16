using StarWarsPlanetsStats;
using StarWarsPlanetsStats.ApiDataAccess;
using StarWarsPlanetsStats.App;
using StarWarsPlanetsStats.DataAccess;
using StarWarsPlanetsStats.Model;
using StarWarsPlanetsStats.UserInteraction;

var apiDataReader = new ApiDataReader();
var mockApiDataReader = new MockStarWarsApiDataReader();
var planetsReader = new RestPlanetsReader(apiDataReader, mockApiDataReader);
var universalTableConsolePrinter = new UniversalTableConsolePrinter();
var userInteractor = new ConsoleUserInteractor();
var planetsStatsUserInteractor = new PlanetsStatsUserInteractor(userInteractor);
var planetStatisticsAnalyzer = new PlanetStatisticsAnalyzer(planetsStatsUserInteractor);

try
{
    await new StarWarsPlanetsStatsApp(planetsReader, universalTableConsolePrinter, planetStatisticsAnalyzer).Run();
}
catch (Exception ex)
{
    Console.WriteLine("An unexpected error occurred: " + ex.Message);
}
