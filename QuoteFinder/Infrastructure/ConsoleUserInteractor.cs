using System.Diagnostics.CodeAnalysis;

namespace QuoteFinder.Infrastructure;

[ExcludeFromCodeCoverage]
public class ConsoleUserInteractor : IUserInteractor
{
    public string ReadInput()
    {
        return Console.ReadLine() ?? string.Empty;
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}
