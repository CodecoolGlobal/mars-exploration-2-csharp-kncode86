using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Validator;

public class ConfigurationValidator : IConfigurationValidator
{
    private ICoordinateCalculator _coordinateCalculator;

    public ConfigurationValidator(ICoordinateCalculator coordinateCalculator)
    {
        _coordinateCalculator = coordinateCalculator;
    }

    public bool Validate(Model.Configuration configuration)
    {
        var pathValidation = ValidatePathToMap(configuration.PathToMap);
        var stepsValidation = configuration.Steps > 0;

        if (!pathValidation || !stepsValidation)
            return false;

        var map = GetMap(configuration.PathToMap);

        var resourcesValidation = configuration.Resources.Any();
        var fileContentValidation = ValidateFileContent(map);
        var landingSpotValidation = ValidateLandingSpot(configuration.LandingSpot, map);


        return resourcesValidation && stepsValidation && pathValidation && landingSpotValidation &&
               fileContentValidation;
    }

    private bool ValidatePathToMap(string path)
    {
        return File.Exists(path);
    }

    private bool ValidateFileContent(Map map)
    {
        var symbols = new List<string> { "#", "&", "*", "%" };

        foreach (var str in map.Representation)
        {
            if (!symbols.Any())
                return true;

            foreach (var mapItem in symbols)
            {
                if (str == mapItem)
                {
                    symbols.Remove(mapItem);
                    break;
                }
            }
        }

        return !symbols.Any();
    }

    private bool ValidateLandingSpot(Coordinate landingSpot, Map map)
    {
        return landingSpot.X < map.Dimension && landingSpot.Y < map.Dimension &&
               landingSpot.X >= 0 && landingSpot.Y >= 0 &&
               _coordinateCalculator.GetAdjacentCoordinates(landingSpot, map.Dimension)
                   .Any(pos => string.IsNullOrWhiteSpace(map.Representation[pos.X, pos.Y]));
    }

    private Map GetMap(string path)
    {
        return new MapLoader.MapLoader().Load(path);
    }
}