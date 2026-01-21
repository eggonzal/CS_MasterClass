using System.Diagnostics.CodeAnalysis;

namespace QuoteFinder.Models;

[ExcludeFromCodeCoverage]
public class Quote
{
    public string QuoteText { get; init; } = string.Empty;
    public string QuoteAuthor { get; init; } = string.Empty;
    public string QuoteGenre { get; init; } = string.Empty;
}