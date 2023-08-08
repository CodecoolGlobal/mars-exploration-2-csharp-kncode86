using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover.Service;

public class RoverDeployer : IRoverDeployer
{
    private ICoordinateCalculator _coordinateCalculator;

    public RoverDeployer(ICoordinateCalculator coordinateCalculator)
    {
        _coordinateCalculator = coordinateCalculator;
    }

    public bool CanPlaceRover(Coordinate spaceShipCoordinate, Map map)
    {
        return _coordinateCalculator.GetAdjacentCoordinates(spaceShipCoordinate, map.Dimension)
            .Any(c => string.IsNullOrWhiteSpace(map.Representation[c.X, c.Y]));
    }

    public Model.MarsRover? PlaceRover(Coordinate spaceShipCoordinate, Map map, int roverSight)
    {
        var validCoordinates = _coordinateCalculator.GetAdjacentCoordinates(spaceShipCoordinate, map.Dimension)
            .Where(c => string.IsNullOrWhiteSpace(map.Representation[c.X, c.Y])).ToArray();

        return validCoordinates.Length < 1 ? null : new Model.MarsRover(validCoordinates[0], roverSight);
    }
}