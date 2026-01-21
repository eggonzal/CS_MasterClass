using System.Diagnostics.CodeAnalysis;

namespace QuoteFinder.Models;

[ExcludeFromCodeCoverage]
public class Pagination
{
    public int currentPage { get; set; }
    public int nextPage { get; set; }
    public int totalPages { get; set; }
}
