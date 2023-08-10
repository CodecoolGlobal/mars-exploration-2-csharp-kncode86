using Codecool.MarsExploration.MapExplorer.Simulation.ExplorationSteps;
using Codecool.MarsExploration.MapExplorer.Simulation.Model;
using Codecool.MarsExploration.MapExplorer.Simulation.MovementRoutines;

namespace Codecool.MarsExploration.MapExplorer.Simulation.Service;

public class SimulationEngine
{
    private readonly ExplorationSimulationSteps _explorationSimulationSteps;
    public SimulationEngine(ExplorationSimulationSteps explorationSimulationSteps)
    {
        _explorationSimulationSteps = explorationSimulationSteps;
    }
    public void RunSimulation(SimulationContext simulationContext)
    {
        
        
        while(_explorationSimulationSteps.Run())
        {
            
        }
        var outcome = _explorationSimulationSteps.ExplorationOutcome;
        Console.WriteLine($"Result of exploration: {outcome}");
        ReturnRoutine routine = new ReturnRoutine();
        Console.WriteLine($"{simulationContext.Rover.Id} is on {simulationContext.Rover.Position} coordinates.");
        routine.TeleportToSpaceShip(simulationContext.Rover, simulationContext.LandingSpot);
        Console.WriteLine("Rover is now teleporting back to the deployer.");
        Console.WriteLine($"{simulationContext.Rover.Id} is at {simulationContext.LandingSpot} now.");
        Console.WriteLine("Exploration done!");

    }
}