using QuoteFinder.Models;

namespace QuoteFinder.DataAccess;

public interface IQuotesProvider
{
    Task<IEnumerable<QuoteCollection>> GetQuotesAsync(int pageCount, int quotesPerPage);
}