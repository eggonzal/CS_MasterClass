namespace GameDataParser.UserInteraction;

public class ConsoleUserInteractor : IUserInteractor
{
    public void PrintError(string message)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = originalColor;
    }

    public void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }

    public string ReadValidFilePath()
    {
        bool isFilePathValid = false;
        string fileName = string.Empty;
        do
        {

            Console.Write("Enter the name of the file you want to read: ");
            fileName = Console.ReadLine();
            if (fileName is null)
            {
                Console.WriteLine("No file name cannot be null.");
            }
            else if (fileName == string.Empty)
            {
                Console.WriteLine("The file name cannot be empty.");
            }
            else if (!File.Exists(fileName))
            {
                Console.WriteLine("The specified file was not found. Please check the file name and try again.");
            }
            else
            {
                isFilePathValid = true;
            }
        } while (!isFilePathValid);
        return fileName;
    }
}