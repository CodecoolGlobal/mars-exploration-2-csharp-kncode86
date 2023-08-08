using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.MapLoader;

public class MapLoader : IMapLoader
{
    public Map Load(string mapFile)
    {
        var fileContent = GetMapFileContent(mapFile);
        var mapRepresentation = GetMapRepresentation(fileContent);

        return new Map(mapRepresentation, true);
    }

    private List<string[]> GetMapFileContent(string path)
    {
        List<string[]> result = new();

        try
        {
            using StreamReader streamReader = new StreamReader(path);

            string? line;

            while ((line = streamReader.ReadLine()) != null)
            {
                var stringArr = line.Select(s => s.ToString()).ToArray();

                result.Add(stringArr);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"MapLoader error: \n{e.Message}\n");
            throw;
        }

        return result;
    }

    private string[,] GetMapRepresentation(List<string[]> list)
    {
        string[,] mapRepresentation = new string[list.Count, list.Count];

        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < list[i].Length; j++)
            {
                mapRepresentation[i, j] = list[i][j];
            }
        }

        return mapRepresentation;
    }
}