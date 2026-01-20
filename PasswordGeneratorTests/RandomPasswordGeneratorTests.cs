using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PasswordGenerator.Generators;
using PasswordGenerator.Utils;
using System.Diagnostics.CodeAnalysis;

namespace Tests.Generators;

[TestFixture]
[ExcludeFromCodeCoverage]
public class RandomPasswordGeneratorTests
{
    private RandomPasswordGenerator _clu;
    private Mock<IRandomProvider> _randomProviderMock;

    [SetUp]
    public void Setup()
    {
        _randomProviderMock = new Mock<IRandomProvider>();
        _clu = new RandomPasswordGenerator(_randomProviderMock.Object);
    }

    [Test]
    public void GenerateWithinRange_ReturnsPasswordOfExpectedLength_WithEmptyConstructorAndSameMaxAndMinLength()
    {
        var generator = new RandomPasswordGenerator();
        int length = 8;
        var password = generator.GenerateWithinRange(length, length, false);
        Assert.That(password.Length, Is.EqualTo(length));
    }

    [Test]
    public void GenerateWithinRange_ThrowsArgumentOutOfRangeException_WhenMinLengthIsLessThanOne()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _clu.GenerateWithinRange(0, 10, true));
    }
    [Test]
    public void GenerateWithinRange_ThrowsArgumentOutOfRangeException_WhenMaxLengthIsLessThanMinLength()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _clu.GenerateWithinRange(10, 5, true));
    }
    [Test]
    public void GenerateWithinRange_ReturnsPasswordOfExpectedLength_AndUsesSpecialCharacters()
    {
        // Arrange
        int minLength = 5;
        int maxLength = 10;
        bool useSpecialCharacters = true;
        int expectedLength = 7;
        _randomProviderMock.Setup(rp => rp.Next(minLength, maxLength + 1)).Returns(expectedLength);
        _randomProviderMock.SetupSequence(rp => rp.Next(It.IsAny<int>()))
            .Returns(0)  // 'A'
            .Returns(26) // '0'
            .Returns(36) // '!'
            .Returns(1)  // 'B'
            .Returns(27) // '1'
            .Returns(37) // '@'
            .Returns(2); // 'C'
        
        // Act
        var password = _clu.GenerateWithinRange(minLength, maxLength, useSpecialCharacters);

        // Assert
        Assert.That(password.Length, Is.EqualTo(expectedLength));
        StringAssert.Contains("A", password);
        StringAssert.Contains("0", password);
        StringAssert.Contains("!", password);
        StringAssert.Contains("B", password);
        StringAssert.Contains("1", password);
        StringAssert.Contains("@", password);
        StringAssert.Contains("C", password);
    }

    [Test]
    public void GenerateWithinRange_ReturnsPasswordOfExpectedLength_WithoutSpecialCharacters()
    {
        // Arrange
        int minLength = 5;
        int maxLength = 10;
        bool useSpecialCharacters = false;
        int expectedLength = 5;
        _randomProviderMock.Setup(rp => rp.Next(minLength, maxLength + 1)).Returns(expectedLength);
        _randomProviderMock.SetupSequence(rp => rp.Next(It.IsAny<int>()))
            .Returns(0)  // 'A'
            .Returns(26) // '0'
            .Returns(1)  // 'B'
            .Returns(27) // '1'
            .Returns(2); // 'C'

        // Act
        var password = _clu.GenerateWithinRange(minLength, maxLength, useSpecialCharacters);

        // Assert
        Assert.That(password.Length, Is.EqualTo(expectedLength));
        StringAssert.Contains("A", password);
        StringAssert.Contains("0", password);
        StringAssert.Contains("B", password);
        StringAssert.Contains("1", password);
        StringAssert.Contains("C", password);
    }
}