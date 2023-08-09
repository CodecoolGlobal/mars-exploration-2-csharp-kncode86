using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Simulation.Model;

public record SimulationContext(int TimeOutSteps, MarsRover.Model.MarsRover Rover, Coordinate LandingSpot, Map Map,
    IEnumerable<string> Resources, bool OutCome, int Steps)
{
    public int Steps { get; set; } = Steps;
}