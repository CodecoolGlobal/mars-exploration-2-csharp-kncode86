using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Simulation.MovementRoutines;

public class ReturnRoutine
{
    private Coordinate _startingCoordinate;

    public ReturnRoutine(Coordinate startingCoordinate)
    {
        _startingCoordinate = startingCoordinate;
    }

    public void TeleportToSpaceShip(MarsRover.Model.MarsRover rover, Coordinate startingCoordinate)
    {
        rover.SetPosition(startingCoordinate);
    }
}