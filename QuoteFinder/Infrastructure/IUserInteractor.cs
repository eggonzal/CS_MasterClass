namespace QuoteFinder.Infrastructure;

public interface IUserInteractor
{
    string ReadInput();
    void ShowMessage(string message);
}
