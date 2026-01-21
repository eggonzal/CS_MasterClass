namespace QuoteFinder.Services;

using QuoteFinder.Models;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

/// <summary>
/// Processes quote collections to find matching quotes containing a search word
/// </summary>
public class QuotesProcessor : IQuotesProcessor
{
    /// <inheritdoc/>
    public IDictionary<int, string?> ProcessSequentially(string word, IEnumerable<QuoteCollection> quoteCollections)
    {
        var results = new Dictionary<int, string?>();

        foreach (var collection in quoteCollections)
        {
            var matchingQuote = FindShortestMatchingQuote(word, collection.Quotes);
            results[collection.Page] = matchingQuote;
        }

        return results;
    }

    /// <inheritdoc/>
    public IDictionary<int, string?> ProcessInParallel(string word, IEnumerable<QuoteCollection> quoteCollections)
    {
        var results = new ConcurrentDictionary<int, string?>();
        var collections = quoteCollections.ToList();

        Parallel.ForEach(collections, collection =>
        {
            var matchingQuote = FindShortestMatchingQuote(word, collection.Quotes);
            results[collection.Page] = matchingQuote;
        });

        return results;
    }

    /// <summary>
    /// Finds the shortest quote containing the search word as a whole word (not partial match)
    /// </summary>
    private static string? FindShortestMatchingQuote(string word, IReadOnlyList<Quote> quotes)
    {
        var wordPattern = CreateWholeWordRegex(word);

        return quotes
            .Where(q => wordPattern.IsMatch(q.QuoteText))
            .OrderBy(q => q.QuoteText.Length)
            .FirstOrDefault()?.QuoteText;
    }

    /// <summary>
    /// Creates a regex pattern that matches the word as a whole word only
    /// </summary>
    private static Regex CreateWholeWordRegex(string word) => 
        new($@"\b{Regex.Escape(word)}\b", RegexOptions.IgnoreCase);
}
