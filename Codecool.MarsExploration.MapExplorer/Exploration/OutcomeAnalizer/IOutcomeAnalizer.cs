using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.OutcomeAnalizer;

public interface IOutcomeAnalizer
{
    bool Timeout(int stepsTaken);
    bool Success(Coordinate coordinate, Map map);
    //bool LackOfResources();
}