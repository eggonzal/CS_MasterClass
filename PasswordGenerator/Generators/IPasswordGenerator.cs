namespace PasswordGenerator.Generators;

public interface IPasswordGenerator
{
    string GenerateWithinRange(int minLength, int maxLength, bool useSpecialCharacters);
}