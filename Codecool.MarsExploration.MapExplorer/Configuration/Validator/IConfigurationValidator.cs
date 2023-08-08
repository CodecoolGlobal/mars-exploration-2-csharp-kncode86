using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Validator;

public interface IConfigurationValidator
{
    bool Validate(Model.Configuration configuration);
}