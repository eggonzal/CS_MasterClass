using FibonacciGenerator;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;

namespace FibonacciGeneratorTests;

[TestFixture]
[ExcludeFromCodeCoverage]
public class FibonacciTests
{
    [Test]
    public void Generate_ThrowsException_WhenInputIsNegative()
    {
        // Arrange
        int n = -1;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Fibonacci.Generate(n));
    }

    [Test]
    public void Generate_ThrowsException_WhenInputIsLargerThanMaxValidNumber()
    {
        // Arrange
        int n = Fibonacci.MaxValidNumber + 1;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Fibonacci.Generate(n));
    }

    [Test]
    public void Generate_DoesNotThrowException_WhenInputIsMaxValidNumber()
    {
        // Arrange
        int n = Fibonacci.MaxValidNumber;

        // Act & Assert
        Assert.DoesNotThrow(() => Fibonacci.Generate(n));
    }

    [TestCaseSource(nameof(GetFibonacciTestCases))]
    public void Generate_ReturnsCorrectFibonacciSequence(int n, IEnumerable<int> expected)
    {
        // Act
        var result = Fibonacci.Generate(n);
        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    private static IEnumerable<object[]> GetFibonacciTestCases()
    {
        return new List<object[]>
        {
            new object[] { 0, new List<int>() },
            new object[] { 1, new List<int> { 0 } },
            new object[] { 2, new List<int> { 0, 1 } },
            new object[] { 3, new List<int> { 0, 1, 1 } },
            new object[] { 6, new List<int> { 0, 1, 1, 2, 3, 5 } },
            new object[] { 10, new List<int> { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 } },
        };
    }
}