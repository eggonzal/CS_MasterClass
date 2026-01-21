using NUnit.Framework;
using QuoteFinder.Models;
using QuoteFinder.Services;
using System.Diagnostics.CodeAnalysis;

namespace QuoteFinderTests.Services;

/// <summary>
/// Tests for the QuotesProcessor
/// </summary>
[TestFixture]
[ExcludeFromCodeCoverage]
public class QuotesProcessorTests
{
    private QuotesProcessor _cut;

    [SetUp]
    public void Setup()
    {
        _cut = new QuotesProcessor();
    }

    #region ProcessSequentially Tests

    [Test]
    public void ProcessSequentially_WhenQuoteContainsWord_ReturnsShortestMatch()
    {
        // Arrange
        var collections = new List<QuoteCollection>
        {
            new()
            {
                Page = 1,
                Quotes = new List<Quote>
                {
                    new() { QuoteText = "Love is patient and kind." },
                    new() { QuoteText = "Love is all." },
                    new() { QuoteText = "Love conquers all things." }
                }
            }
        };

        // Act
        var result = _cut.ProcessSequentially("love", collections);

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[1], Is.EqualTo("Love is all."));
    }

    [Test]
    public void ProcessSequentially_WhenNoQuoteContainsWord_ReturnsNull()
    {
        // Arrange
        var collections = new List<QuoteCollection>
        {
            new()
            {
                Page = 1,
                Quotes = new List<Quote>
                {
                    new() { QuoteText = "Hate is strong." },
                    new() { QuoteText = "Fear is the mind-killer." }
                }
            }
        };

        // Act
        var result = _cut.ProcessSequentially("love", collections);

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[1], Is.Null);
    }

    [Test]
    public void ProcessSequentially_WhenMultiplePages_ReturnsResultForEachPage()
    {
        // Arrange
        var collections = new List<QuoteCollection>
        {
            new()
            {
                Page = 1,
                Quotes = new List<Quote>
                {
                    new() { QuoteText = "Love is all." }
                }
            },
            new()
            {
                Page = 2,
                Quotes = new List<Quote>
                {
                    new() { QuoteText = "Hate is strong." }
                }
            }
        };

        // Act
        var result = _cut.ProcessSequentially("love", collections);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[1], Is.EqualTo("Love is all."));
        Assert.That(result[2], Is.Null);
    }

    [Test]
    public void ProcessSequentially_MatchesWholeWordOnly_DoesNotMatchPartialWord()
    {
        // Arrange - searching for "cat" should NOT match "category"
        var collections = new List<QuoteCollection>
        {
            new()
            {
                Page = 1,
                Quotes = new List<Quote>
                {
                    new() { QuoteText = "This falls into a category." },
                    new() { QuoteText = "The cat sat on the mat." }
                }
            }
        };

        // Act
        var result = _cut.ProcessSequentially("cat", collections);

        // Assert
        Assert.That(result[1], Is.EqualTo("The cat sat on the mat."));
    }

    [Test]
    public void ProcessSequentially_IsCaseInsensitive()
    {
        // Arrange
        var collections = new List<QuoteCollection>
        {
            new()
            {
                Page = 1,
                Quotes = new List<Quote>
                {
                    new() { QuoteText = "LOVE IS ALL." }
                }
            }
        };

        // Act
        var result = _cut.ProcessSequentially("love", collections);

        // Assert
        Assert.That(result[1], Is.EqualTo("LOVE IS ALL."));
    }

    [Test]
    public void ProcessSequentially_WhenEmptyCollections_ReturnsEmptyDictionary()
    {
        // Arrange
        var collections = new List<QuoteCollection>();

        // Act
        var result = _cut.ProcessSequentially("love", collections);

        // Assert
        Assert.That(result.Count, Is.EqualTo(0));
    }

    #endregion

    #region ProcessInParallel Tests

    [Test]
    public void ProcessInParallel_WhenQuoteContainsWord_ReturnsShortestMatch()
    {
        // Arrange
        var collections = new List<QuoteCollection>
        {
            new()
            {
                Page = 1,
                Quotes = new List<Quote>
                {
                    new() { QuoteText = "Love is patient and kind." },
                    new() { QuoteText = "Love is all." },
                    new() { QuoteText = "Love conquers all things." }
                }
            }
        };

        // Act
        var result = _cut.ProcessInParallel("love", collections);

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[1], Is.EqualTo("Love is all."));
    }

    [Test]
    public void ProcessInParallel_WhenNoQuoteContainsWord_ReturnsNull()
    {
        // Arrange
        var collections = new List<QuoteCollection>
        {
            new()
            {
                Page = 1,
                Quotes = new List<Quote>
                {
                    new() { QuoteText = "Hate is strong." },
                    new() { QuoteText = "Fear is the mind-killer." }
                }
            }
        };

        // Act
        var result = _cut.ProcessInParallel("love", collections);

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[1], Is.Null);
    }

    [Test]
    public void ProcessInParallel_WhenMultiplePages_ReturnsResultForEachPage()
    {
        // Arrange
        var collections = new List<QuoteCollection>
        {
            new()
            {
                Page = 1,
                Quotes = new List<Quote>
                {
                    new() { QuoteText = "Love is all." }
                }
            },
            new()
            {
                Page = 2,
                Quotes = new List<Quote>
                {
                    new() { QuoteText = "Hate is strong." }
                }
            },
            new()
            {
                Page = 3,
                Quotes = new List<Quote>
                {
                    new() { QuoteText = "I love programming." }
                }
            }
        };

        // Act
        var result = _cut.ProcessInParallel("love", collections);

        // Assert
        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[1], Is.EqualTo("Love is all."));
        Assert.That(result[2], Is.Null);
        Assert.That(result[3], Is.EqualTo("I love programming."));
    }

    [Test]
    public void ProcessInParallel_MatchesWholeWordOnly_DoesNotMatchPartialWord()
    {
        // Arrange - searching for "cat" should NOT match "category"
        var collections = new List<QuoteCollection>
        {
            new()
            {
                Page = 1,
                Quotes = new List<Quote>
                {
                    new() { QuoteText = "This falls into a category." },
                    new() { QuoteText = "The cat sat on the mat." }
                }
            }
        };

        // Act
        var result = _cut.ProcessInParallel("cat", collections);

        // Assert
        Assert.That(result[1], Is.EqualTo("The cat sat on the mat."));
    }

    [Test]
    public void ProcessInParallel_IsCaseInsensitive()
    {
        // Arrange
        var collections = new List<QuoteCollection>
        {
            new()
            {
                Page = 1,
                Quotes = new List<Quote>
                {
                    new() { QuoteText = "LOVE IS ALL." }
                }
            }
        };

        // Act
        var result = _cut.ProcessInParallel("love", collections);

        // Assert
        Assert.That(result[1], Is.EqualTo("LOVE IS ALL."));
    }

    #endregion
}
