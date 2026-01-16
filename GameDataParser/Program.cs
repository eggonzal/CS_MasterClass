using GameDataParser.App;
using GameDataParser.DataAccess;
using GameDataParser.Logging;
using GameDataParser.UserInteraction;

var consoleInteractor = new ConsoleUserInteractor();
var app = new GameDataParserApp(
    consoleInteractor,
    new GamesPrinter(consoleInteractor),
    new VideoGameDeserializer(consoleInteractor),
    new LocalFileReader());

var logger = new Logger("log.txt");

try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine("Sorry! The application has encountered an unexpected error and needs to close.");
    logger.Log(ex);
}

Console.ReadKey();