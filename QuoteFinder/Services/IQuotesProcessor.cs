namespace QuoteFinder.Services;

using QuoteFinder.Models;

/// <summary>
/// Interface for processing quote collections to find matching quotes
/// </summary>
public interface IQuotesProcessor
{
    /// <summary>
    /// Processes quote collections sequentially to find the shortest quote containing the search word
    /// </summary>
    /// <param name="word">The word to search for</param>
    /// <param name="quoteCollections">The collections of quotes to search</param>
    /// <returns>A dictionary mapping page numbers to the found quote (or null if not found)</returns>
    IDictionary<int, string?> ProcessSequentially(string word, IEnumerable<QuoteCollection> quoteCollections);

    /// <summary>
    /// Processes quote collections in parallel to find the shortest quote containing the search word
    /// </summary>
    /// <param name="word">The word to search for</param>
    /// <param name="quoteCollections">The collections of quotes to search</param>
    /// <returns>A dictionary mapping page numbers to the found quote (or null if not found)</returns>
    IDictionary<int, string?> ProcessInParallel(string word, IEnumerable<QuoteCollection> quoteCollections);
}
