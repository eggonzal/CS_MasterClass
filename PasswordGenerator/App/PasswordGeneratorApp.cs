using PasswordGenerator.Generators;
using System.Diagnostics.CodeAnalysis;

namespace PasswordGenerator.App;

[ExcludeFromCodeCoverage]
public class PasswordGeneratorApp(IPasswordGenerator passwordGenerator)
{
    public void Run()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine(passwordGenerator.GenerateWithinRange(5, 10, false));
        }
        Console.ReadKey();
    }
}