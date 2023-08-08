using Codecool.MarsExploration.MapExplorer.MarsRover.Service;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace MapExplorerTests.MarsRoverTests;

public class RoverDeployerTest
{
    private IRoverDeployer _roverDeployer = new RoverDeployer(new CoordinateCalculator());
    /*
    [Test]
    public void CanPlaceTest()
    {
        //_roverDeployer.CanPlaceRover()
    }
    */
}