namespace QuoteFinder.Services;

public interface IInputValidator
{
    bool IsValidSearchWord(string input);
    bool IsValidPageCount(string input, out int pageCount);
    bool IsValidQuotesPerPage(string input, out int quotesPerPage);
    bool IsValidParallelExecutionChoice(string input, out bool parallelExecutionChoice);
}
