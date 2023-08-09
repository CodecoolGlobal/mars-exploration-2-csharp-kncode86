using Codecool.MarsExploration.MapExplorer.Simulation.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer.Simulation.MovementRoutines;

public class ExplorationRoutine : IMovementRoutine
{
    public Random rnd = new Random();

    public Coordinate NextStep(SimulationContext context)
    {
        var adjacentFreeCoordinates = GetAdjacentFreeCoordinates(context);
        int randomNumber = rnd.Next(0, adjacentFreeCoordinates.Count);
        
        return adjacentFreeCoordinates[randomNumber];
    }

    public List<Coordinate> GetAdjacentFreeCoordinates(SimulationContext context)
    {
        var mapRepresentation = context.Map.Representation;
        List<Coordinate> adjacentFreeCoordinates = new List<Coordinate>();

        if (context.Rover.Position.Y - 1 >= 0 &&
            (mapRepresentation[context.Rover.Position.Y - 1, context.Rover.Position.X] == "" ||
             mapRepresentation[context.Rover.Position.Y - 1, context.Rover.Position.X] == null))
        {
            Coordinate coordinate = new Coordinate(context.Rover.Position.X, context.Rover.Position.Y - 1);
            adjacentFreeCoordinates.Add(coordinate);
        }

        if (context.Rover.Position.Y + 1 >= 0 &&
            (mapRepresentation[context.Rover.Position.Y + 1, context.Rover.Position.X] == "" ||
             mapRepresentation[context.Rover.Position.Y + 1, context.Rover.Position.X] == null))
        {
            var coordinate = new Coordinate(context.Rover.Position.X, context.Rover.Position.Y + 1);
            adjacentFreeCoordinates.Add(coordinate);
        }

        if (context.Rover.Position.X - 1 >= 0 &&
            (mapRepresentation[context.Rover.Position.Y, context.Rover.Position.X - 1] == "" ||
             mapRepresentation[context.Rover.Position.Y, context.Rover.Position.X - 1] == null))
        {
            var coordinate = new Coordinate(context.Rover.Position.X - 1, context.Rover.Position.Y);
            adjacentFreeCoordinates.Add(coordinate);
        }

        if (context.Rover.Position.X + 1 >= 0 &&
            (mapRepresentation[context.Rover.Position.Y, context.Rover.Position.X + 1] == "" ||
             mapRepresentation[context.Rover.Position.Y, context.Rover.Position.X + 1] == null))
        {
            var coordinate = new Coordinate(context.Rover.Position.X + 1, context.Rover.Position.Y);
            adjacentFreeCoordinates.Add(coordinate);
        }


        return adjacentFreeCoordinates;
    }
}