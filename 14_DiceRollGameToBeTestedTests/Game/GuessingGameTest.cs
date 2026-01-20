using Game;
using Moq;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using UserCommunication;

namespace _14_DiceRollGameToBeTestedTests.Game;

[TestFixture]
[ExcludeFromCodeCoverage]
public class GuessingGameTest
{
    private GuessingGame _cut;

    private Mock<IDice> _diceMock;
    private Mock<IUserCommunication> _userCommunicationMock;

    [SetUp]
    public void SetUp()
    {
        _diceMock = new Mock<IDice>();
        _userCommunicationMock = new Mock<IUserCommunication>();
        _cut = new GuessingGame(
            _diceMock.Object,
            _userCommunicationMock.Object);
    }

    [TestCase(4, new[] { 1, 2, 3 })]
    [TestCase(1, new[] { 4, 5, 6 })]
    [TestCase(5, new[] { 1, 1, 1 })]
    public void Play_ReturnsResultLoss_After3WrongGuesses(int roll, int[] userInputs)
    {
        SetupDiceRoll(roll);
        SetupUserInputs(userInputs);
        var result = _cut.Play();
        Assert.That(result, Is.EqualTo(GameResult.Loss));
    }

    [TestCase(1, new[] { 1 })]
    [TestCase(2, new[] { 2 })]
    [TestCase(3, new[] { 3 })]
    [TestCase(4, new[] { 4 })]
    [TestCase(5, new[] { 5 })]
    [TestCase(6, new[] { 6 })]
    [TestCase(2, new[] { 1, 2 })]
    [TestCase(4, new[] { 1, 4 })]
    [TestCase(5, new[] { 1, 3, 5 })]
    public void Play_ReturnsResultVictory_OnCorrectGuess(int roll, int[] userInputs)
    {
        SetupDiceRoll(roll);
        SetupUserInputs(userInputs);

        var result = _cut.Play();
        Assert.That(result, Is.EqualTo(GameResult.Victory));
    }

    private void SetupUserInputs(int[] inputs)
    {
        var inputSequence = _userCommunicationMock.SetupSequence(mock => mock.ReadInteger(It.IsAny<string>()));
        foreach (var input in inputs)
            inputSequence.Returns(input);
    }

    private void SetupDiceRoll(int roll)
    {
        _diceMock.Setup(mock => mock.Roll()).Returns(roll);
    }

    [Test]
    public void PrintResult_ShowsVictoryMessage_OnVictory()
    {
        _cut.PrintResult(GameResult.Victory);
        _userCommunicationMock.Verify(
            mock => mock.ShowMessage("You win!"),
            Times.Once);
    }

    [Test]
    public void PrintResult_ShowsLossMessage_OnLoss()
    {
        _cut.PrintResult(GameResult.Loss);
        _userCommunicationMock.Verify(
            mock => mock.ShowMessage("You lose :("),
            Times.Once);
    }
}   