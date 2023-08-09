using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.OutcomeAnalizer;

public class OutcomeAnalizer : IOutcomeAnalizer
{
    private Configuration.Model.Configuration _configuration;
    private ICoordinateCalculator _coordinateCalculator;

    public OutcomeAnalizer(Configuration.Model.Configuration configuration, ICoordinateCalculator coordinateCalculator)
    {
        _configuration = configuration;
        _coordinateCalculator = coordinateCalculator;
    }

    public bool Timeout(int stepsTaken)
    {
        return _configuration.Steps > stepsTaken;
    }

    public bool Success(Coordinate coordinate, Map map)
    {
        //There is mineral within 5 empty coordinates of water.
        
        if (map.Representation[coordinate.X, coordinate.Y] != "*")
            return false;

        var coordsAroundWater = _coordinateCalculator.GetAdjacentCoordinates(coordinate, map.Dimension, 5);

        return coordsAroundWater.Any(c => map.Representation[c.X, c.Y] == "%");

        // all of the specified resources found in a path:
        //return foundResources.All(resource => _configuration.Resources.Any(res => res == resource));
    }
}