using System;
using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapExplorer.Configuration.Validator;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace MapExplorerTests.ConfigurationTests;

public class ConfigurationValidatorTest
{
    private readonly IConfigurationValidator _configurationValidator = new ConfigurationValidator(new CoordinateCalculator());

    
    [TestCase("exploration-0.map")]
    [TestCase("exploration-1.map")]
    [TestCase("exploration-2.map")]
    public void ConfigurationValidator_PathTest(string path)
    {
        var pathToFile = @$"{Environment.CurrentDirectory}/{path}";
        var config = new Configuration(pathToFile, new Coordinate(0, 0), new[] { "#" }, 10);

        var result = _configurationValidator.Validate(config);
        
        Assert.True(result);
    }
    
    [TestCase("nonexistent1.ne")]
    [TestCase("nonexistent2.ne")]
    [TestCase("nonexistent3.ne")]
    public void ConfigurationValidator_FalsePathTest(string path)
    {
        var pathToFile = @$"{Environment.CurrentDirectory}/{path}";
        var config = new Configuration(pathToFile, new Coordinate(0, 0), new[] { "#" }, 10);

        var result = _configurationValidator.Validate(config);
        
        Assert.False(result);
    }
    
    [TestCase(0,0)]
    [TestCase(16,31)]
    [TestCase(31,31)]
    public void ConfigurationValidator_CoordinatesTest(int x, int y)
    {
        var pathToFile = @$"{Environment.CurrentDirectory}/exploration-0.map";
        var config = new Configuration(pathToFile, new Coordinate(x, y), new[] { "#" }, 10);

        var result = _configurationValidator.Validate(config);
        
        Assert.True(result);
    }
    
    [TestCase(-1,0)]
    [TestCase(16,-310)]
    [TestCase(16,310)]
    [TestCase(310,31)]
    public void ConfigurationValidator_FalseCoordinatesTest(int x, int y)
    {
        var pathToFile = @$"{Environment.CurrentDirectory}/exploration-0.map";
        var config = new Configuration(pathToFile, new Coordinate(x, y), new[] { "#" }, 10);

        var result = _configurationValidator.Validate(config);
        
        Assert.False(result);
    }
    
    [Test]
    public void ConfigurationValidator_ResourcesTest()
    {
        var pathToFile = @$"{Environment.CurrentDirectory}/exploration-0.map";
        var config = new Configuration(pathToFile, new Coordinate(0, 0), new[] { "#", "%", "&", "*" }, 10);

        var result = _configurationValidator.Validate(config);
        
        Assert.True(result);
    }
    
    [Test]
    public void ConfigurationValidator_FalseResourcesTest()
    {
        var pathToFile = @$"{Environment.CurrentDirectory}/exploration-0.map";
        var config = new Configuration(pathToFile, new Coordinate(0, 0), Array.Empty<string>() , 10);

        var result = _configurationValidator.Validate(config);
        
        Assert.False(result);
    }
    
    [Test]
    public void ConfigurationValidator_FalseFileContentTest()
    {
        var pathToFile = @$"{Environment.CurrentDirectory}/exploration-empty.map";
        var config = new Configuration(pathToFile, new Coordinate(0, 0), new[] { "#", "%", "&", "*" } , 10);

        var result = _configurationValidator.Validate(config);
        
        Assert.False(result);
    }
}