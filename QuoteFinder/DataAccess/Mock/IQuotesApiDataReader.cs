namespace QuoteFinder.DataAccess.Mock;

public interface IQuotesApiDataReader : IDisposable
{
    Task<string> ReadAsync(int page, int quotesPerPage);
}