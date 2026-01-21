
using QuoteFinder.DataAccess.Mock;
using QuoteFinder.DTOs;
using QuoteFinder.Models;
using System.Text.Json;

namespace QuoteFinder.DataAccess;

public class QuotesProvider(IQuotesApiDataReader quotesApiDataReader) : IQuotesProvider
{
    private readonly IQuotesApiDataReader _quotesApiDataReader = quotesApiDataReader;

    public async Task<IEnumerable<QuoteCollection>> GetQuotesAsync(int pageCount, int quotesPerPage)
    {
        var tasks = new List<Task<string>>();
        for (int page = 1; page <= pageCount; page++)
            tasks.Add(_quotesApiDataReader.ReadAsync(page, quotesPerPage));
        var results = await Task.WhenAll(tasks);
        return results.Select(ParseJsonRoot);
    }

    private static QuoteCollection ParseJsonRoot(string page)
    {
        var root = JsonSerializer.Deserialize<Root>(page);
        return new QuoteCollection()
        {
            Page = root.pagination.currentPage,
            Quotes = root.data
                .Select(ParseJsonDatum).ToList()
        };
    }

    private static Quote ParseJsonDatum(Datum q)
    {
        return new Quote()
        {
            QuoteText = q.quoteText,
            QuoteAuthor = q.quoteAuthor,
            QuoteGenre = q.quoteGenre
        };
    }
}