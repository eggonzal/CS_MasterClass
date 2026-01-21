using System.Diagnostics.CodeAnalysis;

namespace QuoteFinder.DTOs;

[ExcludeFromCodeCoverage]
public class Datum
{
    public string _id { get; set; }
    public string quoteText { get; set; }
    public string quoteAuthor { get; set; }
    public string quoteGenre { get; set; }
    public int __v { get; set; }
}
