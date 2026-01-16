using StarWarsPlanetsStats.DTOs;
using StarWarsPlanetsStats.Utilities;

namespace StarWarsPlanetsStats.Model;

public readonly record struct Planet
{
    public string Name { get; }
    public int? Diameter { get; }
    public int? SurfaceWater { get; }
    public long? Population { get; }

    public Planet(
        string name,
        int? diameter,
        int? surfaceWater,
        long? population)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Planet name cannot be null or whitespace.", nameof(name));
        }
        Name = name;
        Diameter = diameter;
        SurfaceWater = surfaceWater;
        Population = population;
    }

    public static explicit operator Planet(PlanetDTO planetDTO)
    {
        var name = planetDTO.name;
        var diameter = planetDTO.diameter.ToIntOrNull();
        int? surfaceWater = planetDTO.surface_water.ToIntOrNull();
        long? population = planetDTO.population.ToLongOrNull();
        return new Planet(name, diameter, surfaceWater, population);
    }


}