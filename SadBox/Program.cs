using System.Text.Json;
using System.Text.Json.Serialization;

var baseAddress = "https://api.datausa.io/tesseract/";
var requestUri = "data.jsonrecords?cube=acs_yg_total_population_5&measures=Population&drilldowns=Nation,Year";
var reader = new ApiDataReader();
var data = await reader.Read(baseAddress, requestUri);
var root = JsonSerializer.Deserialize<Root>(data);
foreach(var yearlyData in root!.data)
{
    Console.WriteLine($"Year: {yearlyData.Year}, Population: {yearlyData.Population}");
}

public interface IApiDataReader
{
    Task<string> Read(string baseAddress, string requestUri);
}

public class ApiDataReader : IApiDataReader
{
    public async Task<string> Read(string baseAddress, string requestUri)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(baseAddress);
        HttpResponseMessage response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}

public record Annotations(
    [property: JsonPropertyName("source_description")] string source_description,
    [property: JsonPropertyName("table_id")] string table_id,
    [property: JsonPropertyName("dataset_name")] string dataset_name,
    [property: JsonPropertyName("dataset_link")] string dataset_link,
    [property: JsonPropertyName("subtopic")] string subtopic,
    [property: JsonPropertyName("source_name")] string source_name,
    [property: JsonPropertyName("topic")] string topic
);

public record Datum(
    [property: JsonPropertyName("Nation ID")] string NationID,
    [property: JsonPropertyName("Nation")] string Nation,
    [property: JsonPropertyName("Year")] int Year,
    [property: JsonPropertyName("Population")] double Population
);

public record Page(
    [property: JsonPropertyName("limit")] int limit,
    [property: JsonPropertyName("offset")] int offset,
    [property: JsonPropertyName("total")] int total
);

public record Root(
    [property: JsonPropertyName("annotations")] Annotations annotations,
    [property: JsonPropertyName("page")] Page page,
    [property: JsonPropertyName("columns")] IReadOnlyList<string> columns,
    [property: JsonPropertyName("data")] IReadOnlyList<Datum> data
);

