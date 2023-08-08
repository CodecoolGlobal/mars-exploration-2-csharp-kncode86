using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Model;

public record Configuration(string PathToMap, Coordinate LandingSpot, IEnumerable<string> Resources, int Steps);