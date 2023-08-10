using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.OutcomeAnalizer;

public class OutcomeAnalyzer : IOutcomeAnalyzer
{
    private Configuration.Model.Configuration _configuration;

    public OutcomeAnalyzer(Configuration.Model.Configuration configuration)
    {
        _configuration = configuration;
    }

    public bool Timeout(int stepsTaken)
    {
        return _configuration.Steps > stepsTaken;
    }

    public bool Success(IEnumerable<Coordinate> coordinates, Map map)
    {
        //There are 4 minerals and 3 waters found in total.
        var mineralsCoords = coordinates
            .Where(c => _configuration.Resources.Any(r => r == map.Representation[c.X, c.Y])).ToArray();

        if (!mineralsCoords.Any())
            return false;

        var minerals = mineralsCoords.Select(c => map.Representation[c.X, c.Y]?.ToString() ?? string.Empty);

        var mineralDict = minerals.GroupBy(c => c).ToDictionary(s => s.Key, g => g.Count());

        var isAnyWater = mineralDict.TryGetValue("*", out int waterCount);
        var isAnyMineral = mineralDict.TryGetValue("%", out int mineralCount);

        return isAnyMineral && isAnyWater && waterCount >= 3 && mineralCount >= 4;
    }
}