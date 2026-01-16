namespace GameDataParser.Logging;

public class Logger
{
    private string _logFileName;

    public Logger(string fileName)
    {
        _logFileName = fileName;
    }

    public void Log(Exception ex)
    {
        var entry =
$@"[{DateTime.Now}]
Exception message: {ex.Message}
Stack trace: {ex.StackTrace}

";
        File.AppendAllText(_logFileName, entry);
    }
}