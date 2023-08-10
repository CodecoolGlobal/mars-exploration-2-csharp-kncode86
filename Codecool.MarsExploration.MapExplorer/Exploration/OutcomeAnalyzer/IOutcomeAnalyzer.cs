using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.OutcomeAnalyzer;

public interface IOutcomeAnalyzer
{
    bool Timeout(int stepsTaken);
    bool Success(IEnumerable<Coordinate> coordinates, Map map);
    //bool LackOfResources();
}