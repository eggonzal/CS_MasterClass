
using Moq;
using NUnit.Framework;
using QuoteFinder.App;
using QuoteFinder.DataAccess;
using QuoteFinder.Models;
using QuoteFinder.Services;
using System.Diagnostics.CodeAnalysis;

namespace QuoteFinderTests.App;

/// <summary>
/// Tests for the main application orchestrator
/// </summary>
[TestFixture]
[ExcludeFromCodeCoverage]
public class QuoteFinderAppTests
{
    private QuoteFinderApp _cut;
    private Mock<IUserInputService> _userInputServiceMock;
    private Mock<IQuotesProvider> _quotesProviderMock;

    [SetUp]
    public void Setup()
    {
        _userInputServiceMock = new Mock<IUserInputService>();
        _quotesProviderMock = new Mock<IQuotesProvider>();
        _cut = new QuoteFinderApp(
            _userInputServiceMock.Object,
            _quotesProviderMock.Object);
    }

    [Test]
    public void Run_ShallGetAllUserInputs()
    {
        // Arrange
        _userInputServiceMock.Setup(x => x.GetSearchableWord()).Returns("love");
        _userInputServiceMock.Setup(x => x.GetPageCount()).Returns(3);
        _userInputServiceMock.Setup(x => x.GetQuotesPerPage()).Returns(50);
        _userInputServiceMock.Setup(x => x.GetParallelExecutionChoice()).Returns(true);

        // Act
        _cut.Run();

        // Assert
        _userInputServiceMock.Verify(x => x.GetSearchableWord(), Times.Once);
        _userInputServiceMock.Verify(x => x.GetPageCount(), Times.Once);
        _userInputServiceMock.Verify(x => x.GetQuotesPerPage(), Times.Once);
        _userInputServiceMock.Verify(x => x.GetParallelExecutionChoice(), Times.Once);
    }

    [Test]
    public void Run_ShallProcessAllQuotesSequentially()
    {
        // Arrange
        _userInputServiceMock.Setup(x => x.GetSearchableWord()).Returns("love");
        _userInputServiceMock.Setup(x => x.GetPageCount()).Returns(1);
        _userInputServiceMock.Setup(x => x.GetQuotesPerPage()).Returns(5);
        _userInputServiceMock.Setup(x => x.GetParallelExecutionChoice()).Returns(false);
        _quotesProviderMock.Setup(x => x.GetQuotesAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new List<QuoteCollection>() { 
                new()
                {
                    Page = 1,
                    Quotes = new List<Quote>
                    {
                        new() { QuoteText = "Love is patient." },
                        new() { QuoteText = "Hate is strong." },
                        new() { QuoteText = "Love conquers all." },
                        new() { QuoteText = "Fear is the mind-killer." },
                        new() { QuoteText = "Love thy neighbor." }
                    }
                }
            });

        // Act
        _cut.Run();

        // Assert
        _userInputServiceMock.Verify(x => x.GetSearchableWord(), Times.Once);
        _userInputServiceMock.Verify(x => x.GetPageCount(), Times.Once);
        _userInputServiceMock.Verify(x => x.GetQuotesPerPage(), Times.Once);
        _userInputServiceMock.Verify(x => x.GetParallelExecutionChoice(), Times.Once);
        _quotesProviderMock.Verify(x => x.GetQuotesAsync(1, 5), Times.Once);
    }
}
