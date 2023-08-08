namespace Codecool.MarsExploration.MapExplorer.Logger;

public class FileLogger: ILogger
{
    private readonly string _logFile;

    public FileLogger(string logFile)
    {
        _logFile = logFile;
    }
    
    public void Log(string message)
    {
        var entry = $"[{DateTime.Now}]: {message}";
        using var streamWriter = File.AppendText(_logFile);
        streamWriter.WriteLine(entry);
    }
}