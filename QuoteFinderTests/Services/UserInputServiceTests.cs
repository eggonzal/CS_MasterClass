
using Moq;
using NUnit.Framework;
using QuoteFinder.Infrastructure;
using QuoteFinder.Resources;
using QuoteFinder.Services;
using System.Diagnostics.CodeAnalysis;

namespace QuoteFinderTests.Services;

/// <summary>
/// Tests for the UserInputService - handles prompts, validation, and retries
/// </summary>
[TestFixture]
[ExcludeFromCodeCoverage]
public class UserInputServiceTests
{
    private UserInputService _sut;
    private Mock<IUserInteractor> _userInteractorMock;
    private readonly IInputValidator _inputValidatorMock = new InputValidator();

    [SetUp]
    public void Setup()
    {
        _userInteractorMock = new Mock<IUserInteractor>();
        _sut = new UserInputService(
            _userInteractorMock.Object,
            _inputValidatorMock);
    }

    [Test]
    public void GetSearchableWord_ShallPromptForWord()
    {
        // Arrange
        _userInteractorMock.Setup(x => x.ReadInput()).Returns("love");

        // Act
        _sut.GetSearchableWord();

        // Assert
        _userInteractorMock.Verify(x => x.ShowMessage(QuoteFinderUserPrompts.AskForWordPrompt), Times.Once);
    }

    [Test]
    public void GetSearchableWord_ReturnsWord_WhenValidWordProvided()
    {
        // Arrange
        _userInteractorMock.Setup(x => x.ReadInput()).Returns("love");

        // Act
        var result = _sut.GetSearchableWord();

        // Assert
        Assert.That(result, Is.EqualTo("love"));
    }

    [Test]
    public void GetSearchableWord_PromptsAgain_WhenInvalidWordProvided()
    {
        // Arrange
        _userInteractorMock.SetupSequence(x => x.ReadInput())
            .Returns("invalid123")
            .Returns("love");

        // Act
        var result = _sut.GetSearchableWord();

        // Assert
        _userInteractorMock.Verify(x => x.ShowMessage(It.Is<string>(s => s.Equals(QuoteFinderUserPrompts.InvalidWordMessage))), Times.Once);
        Assert.That(result, Is.EqualTo("love"));
    }

    [Test]
    public void GetPageCount_ReturnsPageCount_WhenValidNumberProvided()
    {
        // Arrange
        _userInteractorMock.Setup(x => x.ReadInput()).Returns("5");
        var result = _sut.GetPageCount();

        // Assert
        Assert.That(result, Is.EqualTo(5));
    }

    [Test]
    public void GetPageCount_ShowsInvalidInputMessage_WhenInvalidNumberProvided()
    {
        // Arrange
        _userInteractorMock.SetupSequence(x => x.ReadInput())
            .Returns("-5")
            .Returns("5");
        var result = _sut.GetPageCount();

        // Assert
        _userInteractorMock.Verify(x => x.ShowMessage(It.Is<string>(s => s.Equals(QuoteFinderUserPrompts.InvalidPageCountMessage))), Times.Once);
    }

    [Test]
    public void GetQuotesPerPage_ReturnsQuotesPerPage_WhenValidNumberProvided()
    {
        // Arrange
        _userInteractorMock.Setup(x => x.ReadInput()).Returns("100");

        // Act
        var result = _sut.GetQuotesPerPage();

        // Assert
        Assert.That(result, Is.EqualTo(100));
    }

    [Test]
    public void GetQuotesPerPage_ShowsInvalidInputMessage_WhenInvalidNumberProvided()
    {
        // Arrange
        _userInteractorMock.SetupSequence(x => x.ReadInput())
            .Returns("-100")
            .Returns("100");

        // Act
        var result = _sut.GetQuotesPerPage();

        // Assert
        _userInteractorMock.Verify(x => x.ShowMessage(It.Is<string>(s => s.Equals(QuoteFinderUserPrompts.InvalidQuotesCountMessage))), Times.Once);
    }

    [TestCase(true, "Y")]
    [TestCase(true, "y")]
    [TestCase(true, " Y")]
    [TestCase(true, "y ")]
    [TestCase(false, "N")]
    [TestCase(false, "n")]
    public void GetParallelExecutionChoice_ReturnsExpectedValue_WhenCorrectInputProvided(bool expected, string input)
    {
        // Arrange
        _userInteractorMock.Setup(x => x.ReadInput()).Returns(input);

        // Act
        var result = _sut.GetParallelExecutionChoice();

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void GetParallelExecutionChoice_ShowsInvalidInputMessage_WhenIncorrectInputProvided()
    {
        // Arrange
        _userInteractorMock.SetupSequence(x => x.ReadInput())
            .Returns("Invalid input :(")
            .Returns("y");

        // Act
        var result = _sut.GetParallelExecutionChoice();

        // Assert
        _userInteractorMock.Verify(x => x.ShowMessage(It.Is<string>(s => s.Equals(QuoteFinderUserPrompts.InvalidParallelExecutionChoiceMessage))), Times.Once);
    }
}
