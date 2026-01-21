using NUnit.Framework;
using QuoteFinder.Services;
using System.Diagnostics.CodeAnalysis;

namespace QuoteFinderTests.Services;

/// <summary>
/// Tests for the InputValidator - pure validation logic
/// </summary>
[TestFixture]
[ExcludeFromCodeCoverage]
public class InputValidatorTests
{
    private InputValidator _sut = new InputValidator();

    [TestCase("love", true)]
    [TestCase("happiness", true)]
    [TestCase("a", true)]
    public void IsValidSearchWord_ReturnsTrue_WhenInputIsValidWord(string input, bool expected)
    {
        var result = _sut.IsValidSearchWord(input);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("love123", false)]
    [TestCase("two words", false)]
    [TestCase("special!", false)]
    [TestCase("", false)]
    [TestCase(null, false)]
    public void IsValidSearchWord_ReturnsFalse_WhenInputIsInvalid(string input, bool expected)
    {
        var result = _sut.IsValidSearchWord(input);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("1", true, 1)]
    [TestCase("10", true, 10)]
    [TestCase("100", true, 100)]
    public void IsValidPageCount_ReturnsTrue_WhenInputIsValidNumber(string input, bool expected, int expectedValue)
    {
        var result = _sut.IsValidPageCount(input, out int pageCount);
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(pageCount, Is.EqualTo(expectedValue));
    }

    [TestCase("0", false)]
    [TestCase("-1", false)]
    [TestCase("abc", false)]
    [TestCase("", false)]
    public void IsValidPageCount_ReturnsFalse_WhenInputIsInvalid(string input, bool expected)
    {
        var result = _sut.IsValidPageCount(input, out _);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("1", true, 1)]
    [TestCase("10", true, 10)]
    [TestCase("100", true, 100)]
    public void IsValidQuotesPerPage_ReturnsTrue_WhenInputIsValidNumber(string input, bool expected, int expectedValue)
    {
        var result = _sut.IsValidQuotesPerPage(input, out int quotesPerPage);
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(quotesPerPage, Is.EqualTo(expectedValue));
    }

    [TestCase("0", false)]
    [TestCase("-1", false)]
    [TestCase("abc", false)]
    [TestCase("", false)]
    public void IsValidQuotesPerPage_ReturnsFalse_WhenInputIsInvalid(string input, bool expected)
    {
        var result = _sut.IsValidQuotesPerPage(input, out _);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("Y", true, true)]
    [TestCase("y", true, true)]
    [TestCase("N", true, false)]
    [TestCase("n", true, false)]
    public void IsValidParallelExecutionChoice_ReturnsTrue_WhenInputIsValidChoice(string input, bool expected, bool expectedValue)
    {
        var result = _sut.IsValidParallelExecutionChoice(input, out bool choice);
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(choice, Is.EqualTo(expectedValue));
    }

    [TestCase("yay", false)]
    [TestCase("nay", false)]
    [TestCase("nope", false)]
    [TestCase("", false)]
    [TestCase(null, false)]
    public void IsValidParallelExecutionChoice_ReturnsFalse_WhenInputIsInvalid(string input, bool expected)
    {
        var result = _sut.IsValidParallelExecutionChoice(input, out _);
        Assert.That(result, Is.EqualTo(expected));
    }
}