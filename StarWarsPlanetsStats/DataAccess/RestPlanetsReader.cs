
using StarWarsPlanetsStats.DTOs;
using StarWarsPlanetsStats.Model;
using System.Text.Json;

namespace StarWarsPlanetsStats.DataAccess;

public class RestPlanetsReader(IApiDataReader apiDataReader, IApiDataReader mockApiDataReader) : IPlanetsReader
{
    private const string ApiBaseAddress = "https://swapi.info/api/";
    private const string PlanetsRequestUri = "planets";
    private readonly IApiDataReader _mockApiDataReader = mockApiDataReader;
    private readonly IApiDataReader _apiDataReader = apiDataReader;
    public async Task<IEnumerable<Planet>> Read()
    {
        string? jsonData = null;
        try
        {
            jsonData = await _apiDataReader.Read(ApiBaseAddress, PlanetsRequestUri);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error while getting the data from the API: {ex.Message}");
            Console.WriteLine("Unable to get data from the API. Will use Mock Data. Error Message: " + ex.Message);
        }

        jsonData ??= await _mockApiDataReader.Read(ApiBaseAddress, PlanetsRequestUri);

        var plantetsStatsDTO = JsonSerializer.Deserialize<List<PlanetDTO>>(jsonData);
        var planets = ToPlanets(plantetsStatsDTO);
        return planets;
    }
    private IEnumerable<Planet> ToPlanets(List<PlanetDTO>? plantetsStatsDTO)
    {
        ArgumentNullException.ThrowIfNull(plantetsStatsDTO);
        return plantetsStatsDTO.Select(dto => (Planet)dto);
    }
}