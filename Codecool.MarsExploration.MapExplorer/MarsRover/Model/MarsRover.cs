using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover.Model;

public class MarsRover
{
    private int _count = 1;
    public string Id { get; init; }
    public Coordinate Position { get; private set; }
    public int Sight { get; init; }
    public HashSet<Coordinate> Encounters { get; init; }

    public MarsRover(Coordinate position, int sight)
    {
        Id = $"rover-{_count}";
        Position = position;
        Sight = sight;
        Encounters = new HashSet<Coordinate>();
        _count++;
    }

    public void SetPosition(Coordinate position)
    {
        Position = position;
    }
}