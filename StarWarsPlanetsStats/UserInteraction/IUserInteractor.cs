namespace StarWarsPlanetsStats.UserInteraction;

public interface IUserInteractor
{
    string? ReadFromUser();
    void ShowMessage(string message);
}