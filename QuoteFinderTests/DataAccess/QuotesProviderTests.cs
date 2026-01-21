using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using QuoteFinder.DataAccess;
using QuoteFinder.DataAccess.Mock;
using QuoteFinder.Models;

namespace QuoteFinderTests.DataAccess;

[TestFixture]
[ExcludeFromCodeCoverage]
public class QuotesProviderTests
{
    [Test]
    public void GetQuotesAsync_ShallReturnQuotes_ForValidPageAndLimit()
    {
        // Arrange
        var sut = new QuotesProvider(new MockQuotesApiDataReader());
        int pageCount = 1;
        int limit = 5;
        // Act
        IEnumerable<QuoteCollection> result = sut.GetQuotesAsync(pageCount, limit).Result;
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(pageCount));
        Assert.That(result.First().Quotes.Count, Is.LessThanOrEqualTo(limit));
    }
}
