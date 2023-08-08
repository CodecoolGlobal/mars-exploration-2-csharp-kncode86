using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Simulation.MovementRoutines;

public interface IMovementRoutine
{
    public Coordinate NextStep(SimulationContext context);
}