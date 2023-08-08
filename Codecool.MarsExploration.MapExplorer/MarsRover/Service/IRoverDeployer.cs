using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover.Service;

public interface IRoverDeployer
{
    bool CanPlaceRover(Coordinate spaceShipCoordinate, Map map);
    Model.MarsRover? PlaceRover(Coordinate spaceShipCoordinate, Map map, int roverSight);
}