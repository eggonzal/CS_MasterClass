using PasswordGenerator.Utils;

namespace PasswordGenerator.Generators;

public class RandomPasswordGenerator(IRandomProvider random) : IPasswordGenerator
{
    private const string AlphaNumPlusSpecialCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_-+=";
    private const string AlphaNumCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private readonly IRandomProvider _random = random;

    public RandomPasswordGenerator() : this(new RandomProvider())
    {
    }

    public string GenerateWithinRange(
        int minLength, int maxLength, bool useSpecialCharacters)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(minLength, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(maxLength, minLength);
        var passwordLength = GetPasswordLength(minLength, maxLength);
        var availableCharacters = GetAvailableCharacters(useSpecialCharacters);
        return GetRandomStringUsingCharacters(availableCharacters, passwordLength);
    }

    private static string GetAvailableCharacters(bool useSpecialCharacters) =>
        useSpecialCharacters ? AlphaNumPlusSpecialCharacters : AlphaNumCharacters;

    private int GetPasswordLength(int minLength, int maxLength) =>
        _random.Next(minLength, maxLength + 1);

    private string GetRandomStringUsingCharacters(string characters, int length) =>
        new(Enumerable.Repeat(characters, length).Select(GetRandomCharacterFromString).ToArray());

    private char GetRandomCharacterFromString(string characters) =>
        characters[_random.Next(characters.Length)];
}