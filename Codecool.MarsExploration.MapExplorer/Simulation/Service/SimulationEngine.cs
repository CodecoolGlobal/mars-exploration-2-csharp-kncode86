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
        
    }
}