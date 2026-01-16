using GameDataParser.Model;

namespace GameDataParser.UserInteraction;

public class GamesPrinter : IGamesPrinter
{
    private readonly IUserInteractor _userInteractor;
    public GamesPrinter(IUserInteractor userInteractor)
    {
        _userInteractor = userInteractor;
    }
    public void Print(List<VideoGame> videoGames)
    {
        if (videoGames.Count > 0)
        {
            _userInteractor.PrintMessage(Environment.NewLine + "Video Games List:" + Environment.NewLine);
            videoGames.ForEach(game => _userInteractor.PrintMessage(game.ToString()));
        }
        else
        {
            _userInteractor.PrintMessage("No games are present in the input file.");
        }
    }
}