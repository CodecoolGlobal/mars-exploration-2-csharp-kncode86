namespace Codecool.MarsExploration.MapExplorer.Logger;

public class ConsoleLogger: ILogger
{
    public void Log(string message)
    {
        var entry = $"[{DateTime.Now}]: {message}";
        Console.WriteLine(entry);
    }
}