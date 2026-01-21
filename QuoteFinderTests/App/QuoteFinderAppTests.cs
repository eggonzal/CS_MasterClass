
using Moq;
using NUnit.Framework;
using QuoteFinder.App;
using QuoteFinder.DataAccess;
using QuoteFinder.Infrastructure;
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
    private Mock<IQuotesProcessor> _quotesProcessorMock;
    private Mock<IUserInteractor> _userInteractorMock;

    [SetUp]
    public void Setup()
    {
        _userInputServiceMock = new Mock<IUserInputService>();
        _quotesProviderMock = new Mock<IQuotesProvider>();
        _quotesProcessorMock = new Mock<IQuotesProcessor>();
        _userInteractorMock = new Mock<IUserInteractor>();
        _cut = new QuoteFinderApp(
            _userInputServiceMock.Object,
            _quotesProviderMock.Object,
            _quotesProcessorMock.Object,
            _userInteractorMock.Object);
    }

    [Test]
    public void Run_ShallGetAllUserInputs()
    {
        // Arrange
        _userInputServiceMock.Setup(x => x.GetSearchableWord()).Returns("love");
        _userInputServiceMock.Setup(x => x.GetPageCount()).Returns(3);
        _userInputServiceMock.Setup(x => x.GetQuotesPerPage()).Returns(50);
        _userInputServiceMock.Setup(x => x.GetParallelExecutionChoice()).Returns(true);
        _quotesProcessorMock.Setup(x => x.ProcessInParallel(It.IsAny<string>(), It.IsAny<IEnumerable<QuoteCollection>>()))
            .Returns(new Dictionary<int, string?>());

        // Act
        _cut.Run();

        // Assert
        _userInputServiceMock.Verify(x => x.GetSearchableWord(), Times.Once);
        _userInputServiceMock.Verify(x => x.GetPageCount(), Times.Once);
        _userInputServiceMock.Verify(x => x.GetQuotesPerPage(), Times.Once);
        _userInputServiceMock.Verify(x => x.GetParallelExecutionChoice(), Times.Once);
    }

    [Test]
    public void Run_WhenParallelIsFalse_ShallProcessQuotesSequentially()
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
                        new() { QuoteText = "Hate is strong." }
                    }
                }
            });
        _quotesProcessorMock.Setup(x => x.ProcessSequentially(It.IsAny<string>(), It.IsAny<IEnumerable<QuoteCollection>>()))
            .Returns(new Dictionary<int, string?> { { 1, "Love is patient." } });

        // Act
        _cut.Run();

        // Assert
        _quotesProviderMock.Verify(x => x.GetQuotesAsync(1, 5), Times.Once);
        _quotesProcessorMock.Verify(x => x.ProcessSequentially("love", It.IsAny<IEnumerable<QuoteCollection>>()), Times.Once);
        _quotesProcessorMock.Verify(x => x.ProcessInParallel(It.IsAny<string>(), It.IsAny<IEnumerable<QuoteCollection>>()), Times.Never);
    }

    [Test]
    public void Run_WhenParallelIsTrue_ShallProcessQuotesInParallel()
    {
        // Arrange
        _userInputServiceMock.Setup(x => x.GetSearchableWord()).Returns("love");
        _userInputServiceMock.Setup(x => x.GetPageCount()).Returns(1);
        _userInputServiceMock.Setup(x => x.GetQuotesPerPage()).Returns(5);
        _userInputServiceMock.Setup(x => x.GetParallelExecutionChoice()).Returns(true);
        _quotesProviderMock.Setup(x => x.GetQuotesAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new List<QuoteCollection>());
        _quotesProcessorMock.Setup(x => x.ProcessInParallel(It.IsAny<string>(), It.IsAny<IEnumerable<QuoteCollection>>()))
            .Returns(new Dictionary<int, string?>());

        // Act
        _cut.Run();

        // Assert
        _quotesProcessorMock.Verify(x => x.ProcessInParallel("love", It.IsAny<IEnumerable<QuoteCollection>>()), Times.Once);
        _quotesProcessorMock.Verify(x => x.ProcessSequentially(It.IsAny<string>(), It.IsAny<IEnumerable<QuoteCollection>>()), Times.Never);
    }

    [Test]
    public void Run_ShallDisplayResultsForEachPage()
    {
        // Arrange
        _userInputServiceMock.Setup(x => x.GetSearchableWord()).Returns("love");
        _userInputServiceMock.Setup(x => x.GetPageCount()).Returns(2);
        _userInputServiceMock.Setup(x => x.GetQuotesPerPage()).Returns(5);
        _userInputServiceMock.Setup(x => x.GetParallelExecutionChoice()).Returns(false);
        _quotesProviderMock.Setup(x => x.GetQuotesAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new List<QuoteCollection>());
        _quotesProcessorMock.Setup(x => x.ProcessSequentially(It.IsAny<string>(), It.IsAny<IEnumerable<QuoteCollection>>()))
            .Returns(new Dictionary<int, string?> 
            { 
                { 1, "Love is patient." },
                { 2, null }
            });

        // Act
        _cut.Run();

        // Assert
        _userInteractorMock.Verify(x => x.ShowMessage("Page 1: Love is patient."), Times.Once);
        _userInteractorMock.Verify(x => x.ShowMessage("Page 2: No matching quote found."), Times.Once);
    }
}
