namespace QuoteFinder.Services;

public class InputValidator : IInputValidator
{
    public bool IsValidSearchWord(string input)
    {
        return !string.IsNullOrEmpty(input) && input.All(char.IsLetter);
    }

    public bool IsValidPageCount(string input, out int pageCount)
    {
        return int.TryParse(input, out pageCount) && pageCount > 0;
    }

    public bool IsValidQuotesPerPage(string input, out int quotesPerPage)
    {
        return int.TryParse(input, out quotesPerPage) && quotesPerPage > 0;
    }

    public bool IsValidParallelExecutionChoice(string input, out bool parallelExecutionChoice)
    {
        return TryParseYesNoChoice(input, out parallelExecutionChoice);
    }

    private static bool TryParseYesNoChoice(string input, out bool parallelExecutionChoice)
    {
        const string y = "y";
        const string n = "n";
        if (string.Equals(input, y, StringComparison.OrdinalIgnoreCase))
        {
            parallelExecutionChoice = true;
            return true;
        }
        if (string.Equals(input, n, StringComparison.OrdinalIgnoreCase))
        {
            parallelExecutionChoice = false;
            return true;
        }
        parallelExecutionChoice = false;
        return false;
    }
}
