using System.Globalization;
using System.Text;
using UglyToad.PdfPig;

try
{
    new TicketsAggregator("Tickets", new FileWriter(), new PdfDocumentsReader()).Run();
}catch(Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

public class TicketsAggregator(string ticketsFolder, IFileWriter fileWriter, IDocumentsReader documentsReader)
{
    private readonly string _ticketsFolder = ticketsFolder;
    private readonly IDocumentsReader _documentsReader = documentsReader;
    private readonly IFileWriter _fileWriter = fileWriter;
    private const string _aggregatedFileName = "aggregatedTickets.txt";
    private static readonly string[] _ticketParts =
        [
            "Title:",
            "Date:",
            "Time:",
            "Visit us:"
        ];
    private static readonly Dictionary<string, CultureInfo> _domainToCulture = new()
    {
        [".com"] = new CultureInfo("en-US"),
        [".fr"] = new CultureInfo("fr-FR"),
        [".jp"] = new CultureInfo("jp-JP"),
    };

    public void Run()
    {
        var tickets = new List<TicketInfo>();
        foreach (var text in _documentsReader.Read(_ticketsFolder))
        {
            var parsedTicketSections = text.Split(_ticketParts, StringSplitOptions.None).Skip(1);
            var culture = _domainToCulture[parsedTicketSections.Last().Trim().ExtractDomain()];
            var ticketData = parsedTicketSections.Take(3).GetEnumerator();
            tickets.Add(
                new TicketInfo(
                               Title: (ticketData.MoveNext() ? ticketData.Current : "N/A"),
                               Date: DateTime.TryParse($"{(ticketData.MoveNext() ? ticketData.Current : "")} {(ticketData.MoveNext() ? ticketData.Current : "")}", culture, out var dt) ? dt : null,
                               Quantity: parsedTicketSections.SkipLast(1).Count() / 3)
            );
        }

        var sb = new StringBuilder();
        foreach (var ticket in tickets)
        {
            Console.Write(ticket.ToString());
            sb.Append(ticket.ToString());
        }
        _fileWriter.WriteTo(sb.ToString(), _ticketsFolder, _aggregatedFileName);
    }
}

public record TicketInfo(
    string Title,
    DateTime? Date,
    int Quantity
)
{
    public override string ToString() => string.Concat(Enumerable.Repeat($"{Title,-60}| {Date?.ToString("d", CultureInfo.InvariantCulture) ?? "N/A",10} | {Date?.ToString("t", CultureInfo.InvariantCulture) ?? "N/A",5}{Environment.NewLine}", Quantity));
}

public interface IDocumentsReader
{
    IEnumerable<string> Read(string directory);
}

public class PdfDocumentsReader : IDocumentsReader
{
    public IEnumerable<string> Read(string directory)
    {
        foreach (var file in Directory.GetFiles(directory, "*.pdf"))
        {
            using var doc = PdfDocument.Open(file);
            yield return doc.GetPage(1).Text;
        }
    }
}

public interface IFileWriter
{
    void WriteTo(string content, params string[] pathParts);
}

public class FileWriter : IFileWriter
{
    public void WriteTo(string content, params string[] pathParts)
    {
        var fullPath = Path.Combine(pathParts);
        File.WriteAllText(fullPath, content);
    }
}

public static class WebAddressExtensions
{
    public static string ExtractDomain(this string webAddress)
    {
        var lastDotIndex = webAddress.LastIndexOf('.');
        return lastDotIndex >= 0 ? webAddress[lastDotIndex..] : string.Empty;
    }
}

public static class FloatingPointNumbersExercise
{
    public static bool IsAverageEqualTo(
        this IEnumerable<double> input, double valueToBeChecked)
    {
        AssertIsNumberArgument(valueToBeChecked, nameof(valueToBeChecked));
        double sum = 0;
        foreach (var inputNumber in input)
        {
            AssertIsNumberArgument(inputNumber, nameof(inputNumber));
            sum += inputNumber;
        }
        
        return Math.Abs((sum / input.Count()) - valueToBeChecked) <= valueToBeChecked;
    }
    private static void AssertIsNumberArgument(double num, string argument = "inputNumber")
    {
        if(num is double.NaN || Math.Abs(num) is double.PositiveInfinity) throw new ArgumentException($"{argument} cannot be NaN or Infinity");
    } 
}

namespace Coding.Exercise
{
    public record struct WeatherData(int? Temperature, int? Humidity);

    public class WeatherDataAggregator
    {
        public IEnumerable<WeatherData> WeatherHistory => _weatherHistory;
        private readonly List<WeatherData> _weatherHistory = [];

        public void GetNotifiedAboutNewData(WeatherData weatherData) => _weatherHistory.Add(weatherData);
    }


    public class WeatherStation
    {
        public event EventHandler<WeatherDataEventArgs>? WeatherMeasured;

        public void Measure()
        {
            int temperature = 25;

            OnWeatherMeasured(temperature);
        }

        private void OnWeatherMeasured(int temperature)
        {
            WeatherMeasured?.Invoke(this, new WeatherDataEventArgs(new WeatherData(temperature, null)));
        }
    }

    public class WeatherBaloon
    {
        public event EventHandler<WeatherDataEventArgs>? WeatherMeasured;

        public void Measure()
        {
            int humidity = 50;

            OnWeatherMeasured(humidity);
        }

        private void OnWeatherMeasured(int humidity)
        {
            WeatherMeasured?.Invoke(this, new WeatherDataEventArgs(new WeatherData(null, humidity)));
        }
    }

    public class WeatherDataEventArgs(WeatherData weatherData) : EventArgs
    {
        public WeatherData WeatherData { get; } = weatherData;
    }
}
