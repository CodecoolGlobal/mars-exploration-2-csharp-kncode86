using Codecool.MarsExploration.MapExplorer.Simulation.Service;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer;

class Program
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;

    public static void Main(string[] args)
    {
        string mapFile = $@"{WorkDir}\Resources\exploration-0.map";
        Coordinate landingSpot = new Coordinate(6, 6);

        var resources = new []{"*"};
        var timeoutSteps = 200;
        var coordinateCalculator = new CoordinateCalculator();

        var configuration =
            new Configuration.Model.Configuration(mapFile, landingSpot, resources, timeoutSteps);
        
        var simulationContextBuilder = new SimulationContextBuilder(configuration, coordinateCalculator);
        var simulationContext = simulationContextBuilder.GetSimulationContext();

        var simulationEngine = new SimulationEngine(simulationContext);
    }
}
