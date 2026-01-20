using PasswordGenerator.App;
using PasswordGenerator.Generators;
using PasswordGenerator.Utils;

var randomPasswordGenerator = new RandomPasswordGenerator(new RandomProvider());
new PasswordGeneratorApp(randomPasswordGenerator).Run();