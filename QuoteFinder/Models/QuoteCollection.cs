using System.Diagnostics.CodeAnalysis;

namespace QuoteFinder.Models;

[ExcludeFromCodeCoverage]
public class QuoteCollection
{
    public int Page { get; init; }
    public IReadOnlyList<Quote> Quotes { get; init; }
}
