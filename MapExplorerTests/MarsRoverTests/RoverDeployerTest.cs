using System.Linq;
using Codecool.MarsExploration.MapExplorer.MarsRover.Service;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace MapExplorerTests.MarsRoverTests;

public class RoverDeployerTest
{
    private IRoverDeployer _roverDeployer = new RoverDeployer(new CoordinateCalculator());
    private ICoordinateCalculator _coordinateCalculator = new CoordinateCalculator();
    
    [Test]
    public void CanPlaceFalseTest()
    {
        var map = new Map(new string?[1,1], true);
        var result = _roverDeployer.CanPlaceRover(new Coordinate(0,0), map);
        
        Assert.False(result);
    }
    
    [Test]
    public void CanPlaceTest()
    {
        var map = new Map(new string?[2,2], true);
        var result = _roverDeployer.CanPlaceRover(new Coordinate(0,0), map);
        
        Assert.True(result);
    }
    
    [Test]
    public void CanPlaceTest_FalseWithObstacles()
    {
        var map = new Map(new string?[2,2], true);
        map.Representation[0, 1] = "#";
        map.Representation[1, 0] = "#";
        map.Representation[1, 1] = "#";
        var result = _roverDeployer.CanPlaceRover(new Coordinate(0,0), map);
        
        Assert.False(result);
    }
    
    [Test]
    public void CanPlaceTest_WithObstacles()
    {
        var map = new Map(new string?[2,2], true);
        map.Representation[1, 0] = "#";
        map.Representation[1, 1] = "#";
        var result = _roverDeployer.CanPlaceRover(new Coordinate(0,0), map);
        
        Assert.True(result);
    }

    [Test]
    public void PlaceTest()
    {
        var map = new Map(new string?[2,2], true);
        var spaceShipCoords = new Coordinate(0, 0);
        var rover = _roverDeployer.PlaceRover(spaceShipCoords, map, 1);

        var eligibleCoords = _coordinateCalculator.GetAdjacentCoordinates(spaceShipCoords, map.Dimension);
        
        Assert.That(eligibleCoords.Any(c => c == rover.Position));
    }
    
    [Test]
    public void PlaceTestFalse()
    {
        var map = new Map(new string?[0,0], true);
        var spaceShipCoords = new Coordinate(0, 0);
        var rover = _roverDeployer.PlaceRover(spaceShipCoords, map, 1);

        Assert.That(rover, Is.Null);
    }
}