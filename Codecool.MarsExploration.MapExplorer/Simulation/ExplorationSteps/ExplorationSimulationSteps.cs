using System.Xml.Schema;
using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapExplorer.Exploration.OutcomeAnalyzer;
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
    private readonly IOutcomeAnalyzer _outcomeAnalyzer;
    private readonly ILogger _logger;
    public ExplorationOutcome? ExplorationOutcome { get; private set; }

    public ExplorationSimulationSteps(SimulationContext simulationContext, ExplorationRoutine routine,
        ICoordinateCalculator coordinateCalculator, IOutcomeAnalyzer outcomeAnalyzer, ILogger logger)
    {
        _simulationContext = simulationContext;
        _routine = routine;
        _coordinateCalculator = coordinateCalculator;
        _outcomeAnalyzer = outcomeAnalyzer;
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
        _simulationContext.Rover.Encounters.UnionWith(resources);

        var analysisResult = Analysis(_simulationContext.Rover.Encounters);
        IncrementStep();
        if (analysisResult)
        {
            ExplorationOutcome = Exploration.ExplorationOutcome.Colonizable;
        }

        return true;
    }

    private IEnumerable<Coordinate> Scan()
    {
        HashSet<Coordinate> coords = _coordinateCalculator
            .GetAdjacentCoordinates(_simulationContext.Rover.Position, _simulationContext.Map.Dimension).ToHashSet();

        for (int i = 1; i < _simulationContext.Rover.Sight - 1; i++)
        {
            var tempHashSet = _coordinateCalculator.GetAdjacentCoordinates(coords, _simulationContext.Map.Dimension)
                .ToHashSet();

            coords.UnionWith(tempHashSet);
        }

        Log(_simulationContext.Steps, "Scanning", _simulationContext.Rover.Position, _simulationContext.Rover.Id);

        return coords;
    }

    private void Movement()
    {
        _simulationContext.Rover.SetPosition(_routine.NextStep(_simulationContext));
        Log(_simulationContext.Steps, "Position change", _simulationContext.Rover.Position,
            _simulationContext.Rover.Id);
    }

    private bool Analysis(IEnumerable<Coordinate> resources)
    {
        bool result = _outcomeAnalyzer.Success(resources, _simulationContext.Map);

        if (result)
        {
            Log(_simulationContext.Steps, "Colonizable", _simulationContext.Rover.Position,
                _simulationContext.Rover.Id);
        }

        return result;
    }

    private bool CheckForTimeOut()
    {
        bool result = _outcomeAnalyzer.Timeout(_simulationContext.Steps);
        string outcome = result ? "Continue exploration" : "Timeout";
        Log(_simulationContext.Steps, outcome, _simulationContext.Rover.Position, _simulationContext.Rover.Id);
        return !result;
    }

    private void Log(int steps, string events, Coordinate position, string roverName)
    {
        string message = $"Step: {steps}; Event {events}; Unit: {roverName}; Position: {position}";
        _logger.Log(message);
    }
}