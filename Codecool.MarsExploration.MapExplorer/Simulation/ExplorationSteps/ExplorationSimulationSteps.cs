using System.Xml.Schema;
using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapExplorer.Exploration.OutcomeAnalizer;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.Simulation.Model;
using Codecool.MarsExploration.MapExplorer.Simulation.MovementRoutines;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer.Simulation.ExplorationSteps;

public class ExplorationSimulationSteps
{
    private readonly SimulationContext _simulationContext;
    private readonly ExplorationRoutine _routine;
    private readonly ICoordinateCalculator _coordinateCalculator;
    private readonly IOutcomeAnalizer _outcomeAnalizer;
    private readonly ILogger _logger;
    public ExplorationOutcome? ExplorationOutcome { get; private set; }
    public ExplorationSimulationSteps(SimulationContext simulationContext, ExplorationRoutine routine, ICoordinateCalculator coordinateCalculator, IOutcomeAnalizer outcomeAnalizer, ILogger logger)
    {
        _simulationContext = simulationContext;
        _routine = routine;
        _coordinateCalculator = coordinateCalculator;
        _outcomeAnalizer = outcomeAnalizer;
        _logger = logger;
        ExplorationOutcome = null;
    }

    private void IncrementStep()
    {
        _simulationContext.Steps += 1;
    }

    public bool Run()
    {
        if (CheckForTimeOut())
        {
            ExplorationOutcome = ExplorationOutcome ?? Exploration.ExplorationOutcome.Timeout;
            return false;
        }
        Movement();
        var resources = Scan();
        var analysisResult = Analysis(resources);
        IncrementStep();
        if (analysisResult)
        {
            ExplorationOutcome = Exploration.ExplorationOutcome.Colonizable;
        }

        return true;

    }

    private void Movement()
    {
        _simulationContext.Rover.SetPosition(_routine.NextStep(_simulationContext));
        Log(_simulationContext.Steps, "Position change", _simulationContext.Rover.Position, _simulationContext.Rover.Id);
    }

    private IEnumerable<Coordinate> Scan()
    {
        var scannedCoordinates = _coordinateCalculator.GetAdjacentCoordinates(_simulationContext.Rover.Position,
            _simulationContext.Map.Dimension, _simulationContext.Rover.Sight);
        var result = scannedCoordinates.Where(c =>
            _simulationContext.Resources.Any(r => r == _simulationContext.Map.Representation[c.Y, c.X]));
        Log(_simulationContext.Steps, "Scanning", _simulationContext.Rover.Position, _simulationContext.Rover.Id);
        return result;

    }

    private bool Analysis(IEnumerable<Coordinate> resources)
    { 
        bool result = resources.Any(r => _outcomeAnalizer.Success(r, _simulationContext.Map));
        string outcome = result ? "Colonizable" : "Unsuccessful analysis";
        
        Log(_simulationContext.Steps, outcome, _simulationContext.Rover.Position, _simulationContext.Rover.Id);
        return result;

    }

    private bool CheckForTimeOut()
    {
        bool result = _outcomeAnalizer.Timeout(_simulationContext.Steps);
        string outcome = result ? "Timeout" : "Continue exploration";
        Log(_simulationContext.Steps, outcome, _simulationContext.Rover.Position, _simulationContext.Rover.Id);
        return result;
    }

    private void Log(int steps, string events, Coordinate position, string roverName)
    {
        string message = $"Step: {steps}; Event {events}; Unit: {roverName}; Position: {position}";
        _logger.Log(message);
    }
    
}