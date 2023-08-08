using System;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace MapExplorerTests.ConfigurationTests;

public class Tests
{
    [Test]
    public void MapLoader()
    {
        var path = Environment.CurrentDirectory;
        IMapLoader mapLoader = new MapLoader();
        var map = mapLoader.Load($@"{path}/exploration-0.map");

        int count = 0;
        Console.WriteLine(map.Representation.Length);

        var dimension = new DimensionCalculator().CalculateDimension(map.Representation.Length, 0);
        
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                if (map.Representation[i, j] == "#")
                    count++;
                
                
            }
        }
        
        Assert.That(count, Is.EqualTo(70));
    }
}