namespace QuoteFinder.App;

using QuoteFinder.DataAccess;
using QuoteFinder.Infrastructure;
using QuoteFinder.Services;

/// <summary>
/// Main application orchestrator - coordinates the workflow
/// </summary>
public class QuoteFinderApp(
    IUserInputService userInputService,
    IQuotesProvider quotesProvider,
    IQuotesProcessor quotesProcessor,
    IUserInteractor userInteractor)
{
    private readonly IUserInputService _userInputService = userInputService;
    private readonly IQuotesProvider _quotesProvider = quotesProvider;
    private readonly IQuotesProcessor _quotesProcessor = quotesProcessor;
    private readonly IUserInteractor _userInteractor = userInteractor;

    public void Run()
    {
        var word = _userInputService.GetSearchableWord();
        var pageCount = _userInputService.GetPageCount();
        var quotesPerPage = _userInputService.GetQuotesPerPage();
        bool shouldProcessInParallel = _userInputService.GetParallelExecutionChoice();

        var quotesTask = _quotesProvider.GetQuotesAsync(pageCount, quotesPerPage);
        var quotes = quotesTask.Result;

        var results = shouldProcessInParallel
            ? _quotesProcessor.ProcessInParallel(word, quotes)
            : _quotesProcessor.ProcessSequentially(word, quotes);

        DisplayResults(results);
    }

    private void DisplayResults(IDictionary<int, string?> results)
    {
        foreach (var (page, quote) in results.OrderBy(r => r.Key))
        {
            if (quote is not null)
            {
                _userInteractor.ShowMessage($"Page {page}: {quote}");
            }
            else
            {
                _userInteractor.ShowMessage($"Page {page}: No matching quote found.");
            }
        }
    }
}
