namespace QuoteFinder.Services;

public interface IUserInputService
{
    string GetSearchableWord();
    int GetPageCount();
    int GetQuotesPerPage();
    bool GetParallelExecutionChoice();
}
