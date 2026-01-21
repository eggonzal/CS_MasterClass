namespace QuoteFinder.App;

using QuoteFinder.DataAccess;
using QuoteFinder.Models;
using QuoteFinder.Services;

/// <summary>
/// Main application orchestrator - coordinates the workflow
/// </summary>
public class QuoteFinderApp(IUserInputService userInputService, IQuotesProvider quotesProvider)
{
    private readonly IUserInputService _userInputService = userInputService;
    private readonly IQuotesProvider _quotesProvider = quotesProvider;

    public void Run()
    {
        var word = _userInputService.GetSearchableWord();
        var pageCount = _userInputService.GetPageCount();
        var quotesPerPage = _userInputService.GetQuotesPerPage();
        bool shouldProcessInParallel = _userInputService.GetParallelExecutionChoice();
        var quotesTask = _quotesProvider.GetQuotesAsync(pageCount, quotesPerPage);
        var quotes = quotesTask.Result;
        if (shouldProcessInParallel)
        {
            ProcessQuotesSequentially(word, quotes);
        }
        else
        {
            ProcessQuotesSequentially(word, quotes);
        }
    }

    private string ProcessQuotesSequentially(string word, IEnumerable<QuoteCollection> quoteCollections)
    {
        return quoteCollections.SelectMany(qc => qc.Quotes)
            .Where(q => q.QuoteText.Contains(word, StringComparison.OrdinalIgnoreCase))
            .OrderBy(quote => quote.QuoteText.Length)
            .FirstOrDefault()?.QuoteText ?? string.Empty;
    }
}
