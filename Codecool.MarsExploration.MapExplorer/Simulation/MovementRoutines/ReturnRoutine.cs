using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Simulation.MovementRoutines;

public class ReturnRoutine
{
    public void TeleportToSpaceShip(MarsRover.Model.MarsRover rover, Coordinate startingCoordinate)
    {
        rover.SetPosition(startingCoordinate);
    }

    public void MoveBackToSpaceShip(MarsRover.Model.MarsRover rover, IEnumerable<Coordinate> pathTaken)
    {
        IEnumerable<Coordinate> pathBack = pathTaken.Reverse();

        foreach (var coordinate in pathBack)
        {
            rover.SetPosition(coordinate);
        }
    }
}