using StarWarsPlanetsStats.Model;

namespace StarWarsPlanetsStats.UserInteraction;

public class PlanetsStatsUserInteractor(IUserInteractor userInteractor) : IPlanetsStatsUserInteractor
{
    private readonly IUserInteractor _userInteractor = userInteractor;
    public void Show(IEnumerable<Planet> planets)
    {
        foreach (var planet in planets)
        {
            Console.WriteLine(planet);
        }
    }
    public string? ChooseStatisticsToBeShown(IEnumerable<string> propertiesThatCanBeChosen)
    {
        ShowMessage(Environment.NewLine);
        ShowMessage("The statistics of which property would you like to see? (" + string.Join(", ", propertiesThatCanBeChosen) + "): ");
        return _userInteractor.ReadFromUser();
    }
    public void ShowMessage(string message)
    {
        _userInteractor.ShowMessage(message);
    }
}