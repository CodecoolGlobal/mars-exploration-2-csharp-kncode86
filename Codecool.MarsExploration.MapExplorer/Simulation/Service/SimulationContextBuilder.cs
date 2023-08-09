using Codecool.MarsExploration.MapExplorer.MarsRover.Service;
using Codecool.MarsExploration.MapExplorer.Simulation.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Simulation.Service;

public class SimulationContextBuilder
{
    private readonly Configuration.Model.Configuration _configuration;
    private readonly ICoordinateCalculator _coordinateCalculator;

    public SimulationContextBuilder(Configuration.Model.Configuration configuration, ICoordinateCalculator coordinateCalculator)
    {
        _configuration = configuration;
        _coordinateCalculator = coordinateCalculator;
    }

    private MarsRover.Model.MarsRover GetMarsRover(int sight, Coordinate startingCoordinate)
    {
        int count = 0;
        var rover = new RoverDeployer(_coordinateCalculator);
        Coordinate coordinate = startingCoordinate;
        Map map = GetMap();
        
        while (!rover.CanPlaceRover(coordinate, map) && count < 100)
        {
            coordinate = _coordinateCalculator.GetRandomCoordinate(map.Dimension);
            count++;
        }

        return rover.PlaceRover(coordinate, map, sight)!;
    }

    private Map GetMap()
    {
        var map = new MapLoader.MapLoader();
        return map.Load(_configuration.PathToMap);
    }

    public SimulationContext GetSimulationContext()
    {
        MarsRover.Model.MarsRover rover = GetMarsRover(5, _configuration.LandingSpot);
        return new SimulationContext(_configuration.Steps, rover , _configuration.LandingSpot, GetMap(), _configuration.Resources, false, 0);
    }
}